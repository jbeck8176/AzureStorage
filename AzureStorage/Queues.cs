using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureStorage
{
    public class Queues
    {
        private CloudStorageAccount _account;
        private string queueName;

        /// <summary>
		/// Initializes the Tables class using the supplied connection string.
		/// </summary>
		/// <param name="connectionString">The connection string from the Azure portal.</param>
		public Queues(string connectionString, string queueName)
        {
            _account = CloudStorageAccount.Parse(connectionString);
            this.queueName = queueName.ToLower();
        }


        private CloudQueueClient _queueClient = null;
        private CloudQueueClient QueueClient
        {
            get
            {
                if (_queueClient != null)
                {
                    return _queueClient;
                }
                _queueClient = _account.CreateCloudQueueClient();
                return _queueClient;
            }
        }

        /// <summary>
        /// Adds message to queue
        /// </summary>
        /// <param name="queueMessage"></param>
        public void Insert(string queueMessage)
        {
            //call the overloaded method
            Insert(queueMessage, 0);
        }

        /// <summary>
        /// Adds message to queue but delays the visibility
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <param name="initialDelay">Seconds to delay the visibility.</param>
        public void Insert(string queueMessage, int initialDelay)
        {
            if (initialDelay < 0)
            {
                initialDelay = 0;
            }

            // Retrieve a reference to a queue.
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(queueMessage);
            queue.AddMessage(message, initialVisibilityDelay: TimeSpan.FromSeconds(initialDelay));
        }

        /// <summary>
        /// Use to look at the next message in line with out removing it from the queue.
        /// </summary>
        /// <returns></returns>
        public CloudQueueMessage Peek()
        {
            // Retrieve a reference to a queue
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            // Peek at the next message
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            // return message.
            return peekedMessage;
        }

        /// <summary>
        /// Gets the next message from the queue. This will make the message invisable to all other accessors.
        /// </summary>
        /// <param name="visabilityTimeout">Optional parameter that will set the invisability range. Defaults to 5 minutes.</param>
        /// <returns></returns>
        public CloudQueueMessage GetNextMessage(TimeSpan? visabilityTimeout = null)
        {
            //set timeout default
            var timeout = TimeSpan.FromMinutes(5);
            if (visabilityTimeout.HasValue)
                timeout = visabilityTimeout.Value;

            // Retrieve a reference to a queue
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            // get the next message
            CloudQueueMessage nextMessage = queue.GetMessage(timeout);

            // return message
            return nextMessage;
        }

        /// <summary>
        /// Gets next messages in a batch. BatchMax size 32.
        /// </summary>
        /// <param name="batchCount">Batch size, max size = 32</param>
        /// <param name="visabilityTimeout">Defaut to 5 min.</param>
        /// <returns></returns>
        public List<CloudQueueMessage> GetNextMessageBatch(int batchCount, TimeSpan? visabilityTimeout = null)
        {
            //set timeout default
            var timeout = TimeSpan.FromMinutes(5);
            if (visabilityTimeout.HasValue)
                timeout = visabilityTimeout.Value;

            if (batchCount > 32)
                batchCount = 32;

            // Retrieve a reference to a queue
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            // get the next message
            var nextMessages = queue.GetMessages(batchCount, timeout).ToList();

            // return message
            return nextMessages;
        }

        public void DeQueue(CloudQueueMessage message)
        {
            // Retrieve a reference to a queue
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            //delete the message
            queue.DeleteMessage(message);
        }

        public int queueLength()
        {
            // Retrieve a reference to a queue.
            CloudQueue queue = QueueClient.GetQueueReference(this.queueName);

            // Fetch the queue attributes.
            queue.FetchAttributes();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = queue.ApproximateMessageCount;

            // return number of messages.
            return cachedMessageCount.HasValue ? cachedMessageCount.Value : 0;
        }
    }
}

