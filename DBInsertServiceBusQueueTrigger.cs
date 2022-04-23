using System;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace POCAzureFunctions
{
    public static class DBInsertServiceBusQueueTrigger
    {
        
        public class User
        {
            public string FullName { get; set; }
            public string Age { get; set; }
        }

        [FunctionName("DBInsertServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger("myqueue", Connection = "pocazurefunction_SERVICEBUS")]string jsonString, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {jsonString}");
            try{

                // MYSQL Client
                string connectionString = @"server=localhost;userid=root;password=test;database=demo";
                using var mysqlClient = new MySqlConnection(connectionString);
                // Parse JSON String
                var users = JsonSerializer.Deserialize<User[]>(jsonString);

                //Read Users Arr
                foreach (var user in users)
                {
                    var Uid = Guid.NewGuid().ToString();
                    var FullName = user.FullName;
                    var Age = user.Age;
                    log.LogInformation($"User: {Uid} - {FullName} - {Age}");
                    MySqlCommand cmd = new MySqlCommand($"CALL postUser('{Uid}', '{FullName}', {Age})");
                    cmd.Connection = mysqlClient;
                    mysqlClient.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var Id = reader.GetGuid(0);
                            log.LogInformation($"Id: {Id}");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Unable to insert user.");
                    }
                    reader.Close();
                    cmd.Connection.Close();
                }
            }
            catch(Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
