using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureStorage.DemoApplication
{
    public partial class Form1 : Form
    {
        private List<string> queueStatus = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStInsert_Click(object sender, EventArgs e)
        {
            //build some test data to save into the storage table
            var testData = new TestObject()
            {
                intProperty = 1,
                stringProperty = "test string",
                boolProperty = false,
                datetimeOffsetProperty = DateTimeOffset.Now,
                dateTimeProperty = DateTime.Now,
                doubleProperty = 12.34,
                guidProperty = Guid.NewGuid(),
                binaryProperty = Encoding.ASCII.GetBytes("test binary"),
                PartitionKey = tbPartitionKey.Text,
                RowKey = tbRowKey.Text
            };

            //init the storage table
            var storTable = new AzureStorage.Tables(Properties.Settings.Default.AzureConnection);
            //add the data or update it to the storage table
            storTable.InsertOrReplace("UnitTestTable", testData);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbPartitionKey.Text = Guid.NewGuid().ToString().ToUpper();
            tbQPartitionKey.Text = tbPartitionKey.Text;
            
        }

        private void btnRetStorageTableData_Click(object sender, EventArgs e)
        {
            //init the storage table
            var storTable = new AzureStorage.Tables(Properties.Settings.Default.AzureConnection);
            //retrieve from the storage table
            var stTableObj = storTable.Retrieve<TestObject>("UnitTestTable", tbRowKey.Text, tbPartitionKey.Text);
            //show the data in the text box
            tbStResView.Text = JsonConvert.SerializeObject(stTableObj);
        }

        private void btnProcTestQueue_Click(object sender, EventArgs e)
        {
            //disable the button 
            btnProcTestQueue.Enabled = false;

            //init the queue
            var queue = new Queues(Properties.Settings.Default.AzureConnection, "testqueue");

            //build a object to be serialized and sent to the queue
            var obj = new QueueObject() {PartitionKey = tbQPartitionKey.Text, RowKey = tbQRowKey.Text };

            //add it to the queue
            queue.Insert(JsonConvert.SerializeObject(obj), 20);

            //lets go ahead and start a task to wait for the queue and process it
            var taskList = new List<Task>();
            
            var task = StartThread();
            taskList.Add(task);

            var context = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.ContinueWhenAll(taskList.ToArray(), ThreadsComplete, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, context);
        }

        /// <summary>
        /// starts the thread and has a delegate that handles the updates from the task
        /// </summary>
        /// <returns></returns>
        private Task StartThread()
        {
            // handle the progress updates
            Progress<List<string>> progress = new Progress<List<string>>();
            progress.ProgressChanged += (insender, progressObj) =>
            {
                rtbQueueRes.Lines = progressObj.ToArray();
            };

            //start the task
            var task = Task.Factory.StartNew(() => RunProcess(progress));

            return task;
        }

        private void RunProcess(IProgress<List<string>> progress)
        {
            //init the queue
            var queue = new Queues(Properties.Settings.Default.AzureConnection, "testqueue");

            var updateList = new List<string>();
            var done = false;
            var ctr = 20;

            while (!done)
            {
                //check queue
                updateList.Add("Checking for queue");
                progress.Report(updateList);
                
                var queueObj = queue.GetNextMessage(TimeSpan.FromSeconds(20));
                
                //process if there was one found
                if (queueObj != null)
                {
                    updateList.Add("Queue item found: " + queueObj.AsString);
                    progress.Report(updateList);

                    updateList.Add("Process");
                    progress.Report(updateList);

                    updateList.Add("De-queue");
                    progress.Report(updateList);

                    queue.DeQueue(queueObj);
                    
                    updateList.Add("Queue finished");
                    progress.Report(updateList);

                    done = true;
                }

                if (!done)
                {
                    Thread.Sleep(5000);
                    ctr--;
                    if (ctr == 0)
                        done = true;
                }
            }

            updateList.Add("done");
            progress.Report(updateList);
        }

        private void ThreadsComplete(Task[] tasks)
        {
            btnProcTestQueue.Enabled = true;
        }
    }
}
