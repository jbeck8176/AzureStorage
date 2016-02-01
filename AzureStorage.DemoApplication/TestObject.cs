using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AzureStorage.DemoApplication
{
    public class TestObject : TableEntity
    {
        public int intProperty { get; set; }
        public string stringProperty { get; set; }
        public Guid guidProperty { get; set; }
        public bool boolProperty { get; set; }
        public DateTime dateTimeProperty { get; set; }
        public DateTimeOffset datetimeOffsetProperty { get; set; }
        public double doubleProperty { get; set; }
        public byte[] binaryProperty { get; set; }
    }
}
