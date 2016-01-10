using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorage.Tests
{
    [TestClass()]
    public class TablesTests
    {
        private UnitTestObject testData = new UnitTestObject()
        {
            intProperty = 1,
            stringProperty = "test string",
            boolProperty = false,
            datetimeOffsetProperty = DateTimeOffset.Now,
            dateTimeProperty = DateTime.Now,
            doubleProperty = 12.34,
            guidProperty = Guid.NewGuid(),
            binaryProperty = Encoding.ASCII.GetBytes("test binary")
        };

        public const string tableName = "UnitTestTable";
        public string partitionKey = Guid.NewGuid().ToString();
        public string rowKey = Guid.NewGuid().ToString();

        [TestMethod()]
        public void InsertOrReplaceTest()
        {

            try
            {
                var tables = new AzureStorage.Tables(Properties.Settings.Default.AzureConnection);
                tables.InsertOrReplace(tableName, rowKey, partitionKey, testData);
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to write to storage table: " + e.Message);
                throw e;
            }

        }

        [TestMethod()]
        public void RetrieveTest()
        {
            try
            {
                InsertOrReplaceTest();
                var tables = new AzureStorage.Tables(Properties.Settings.Default.AzureConnection);

                var tblResult = tables.Retrieve(tableName, rowKey, partitionKey);

                Assert.AreNotEqual(tblResult.HttpStatusCode, 404);

                var tblEntity = ((Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity)(tblResult.Result));

                Assert.IsNotNull(tblResult);

                CollectionAssert.AreEqual(testData.binaryProperty, tblEntity.Properties["binaryProperty"].BinaryValue);
                Assert.AreEqual(testData.boolProperty, tblEntity.Properties["boolProperty"].BooleanValue);
                //Assert.AreEqual(testData.datetimeOffsetProperty, tblEntity.Properties["datetimeOffsetProperty"].DateTimeOffsetValue);
                //Assert.AreEqual(testData.dateTimeProperty, tblEntity.Properties["dateTimeProperty"].DateTime);
                Assert.AreEqual(testData.doubleProperty, tblEntity.Properties["doubleProperty"].DoubleValue);
                Assert.AreEqual(testData.guidProperty, tblEntity.Properties["guidProperty"].GuidValue);
                Assert.AreEqual(testData.intProperty, tblEntity.Properties["intProperty"].Int32Value);
                Assert.AreEqual(testData.stringProperty, tblEntity.Properties["stringProperty"].StringValue);
            }
            catch (Exception e)
            {
                Assert.Fail("Retrieve test failed: " + e.Message);
            }
        }

        [TestMethod()]
        public void RetrieveTestGenericTypes()
        {
            try
            {
                InsertOrReplaceTest();
                var tables = new AzureStorage.Tables(Properties.Settings.Default.AzureConnection);

                var retObj = tables.Retrieve<UnitTestObject>(tableName, rowKey, partitionKey);

                CollectionAssert.AreEqual(testData.binaryProperty, retObj.binaryProperty);
                Assert.AreEqual(testData.boolProperty, retObj.boolProperty);
                //Assert.AreEqual(testData.datetimeOffsetProperty, tblEntity.Properties["datetimeOffsetProperty"].DateTimeOffsetValue);
                //Assert.AreEqual(testData.dateTimeProperty, tblEntity.Properties["dateTimeProperty"].DateTime);
                Assert.AreEqual(testData.doubleProperty, retObj.doubleProperty);
                Assert.AreEqual(testData.guidProperty, retObj.guidProperty);
                Assert.AreEqual(testData.intProperty, retObj.intProperty);
                Assert.AreEqual(testData.stringProperty, retObj.stringProperty);
            }
            catch (Exception e)
            {
                Assert.Fail("Retrieve test failed: " + e.Message);
            }
        }
    }

    public class UnitTestObject : TableEntity
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