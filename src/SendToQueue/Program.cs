using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace SendToQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please provide fireworks color");
            string color = Console.ReadLine();

            Console.WriteLine("Please provide number of fireworks");
            int fireworksCount = int.Parse(Console.ReadLine());

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("keda");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            for(int i=0; i < fireworksCount; i++)
            {
                // Create a message and add it to the queue.
                CloudQueueMessage message = new CloudQueueMessage(color);
                queue.AddMessage(message);
            }
        }
    }
}
