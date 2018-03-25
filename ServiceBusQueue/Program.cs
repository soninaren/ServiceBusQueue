using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusQueue
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "CONNECTIONSTRING";

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(connectionString);
            bool queueExists = namespaceManager.QueueExists("QUEUENANE");
            if (!queueExists)
            {
                namespaceManager.CreateQueue("QUEUENANE");
            }

            QueueClient Client = QueueClient.CreateFromConnectionString(connectionString, "QUEUENANE");

            for (int i = 0; i < 5; i++)
            {
                // Create message, passing a string message for the body.
                BrokeredMessage message = new BrokeredMessage("Test message " + i);

                // Set some addtional custom app-specific properties.
                message.Properties["TestProperty"] = "TestValue";
                message.Properties["Message number"] = i;

                // Send message to the queue.
                Client.Send(message);
            }

        }
    }
}
