using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.DemoApplication
{
    class QueueObject
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}
