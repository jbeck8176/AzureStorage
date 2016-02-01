using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace AzureStorage
{
	public class Tables
	{
		private CloudStorageAccount _account;
		//Lets keep a dictionary of tables
		private Dictionary<string, CloudTable> tables = new Dictionary<string, CloudTable>();

		/// <summary>
		/// Initializes the Tables class using the supplied connection string.
		/// </summary>
		/// <param name="connectionString">The connection string from the Azure portal.</param>
		public Tables(string connectionString)
		{
			_account = CloudStorageAccount.Parse(connectionString);
		}

		/// <summary>
		/// Gets the CloudTable by name.
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>The cloud table reference</returns>
		private CloudTable GetTable(string Name)
		{
			// If the table reference exists in the dictionary, return it
			string table = Name.ToString().ToLower().Trim();
			if (tables.ContainsKey(table))
			{
				return tables[table];
			}

			// Since it doesn't, create a reference to it
			CloudTableClient tableClient = _account.CreateCloudTableClient();
			CloudTable currentTable = tableClient.GetTableReference(table);

			// Add to dictionary and return
			tables.Add(table, currentTable);
			return currentTable;
		}

		public void InsertOrReplace(string tableName, string rowKey, string partitionKey, ITableEntity row)
		{
			//handle override for when I want to send in my own row key and partition key
			row.PartitionKey = partitionKey;
			row.RowKey = rowKey;

			InsertOrReplace(tableName, row);
		}

		public void InsertOrReplace(string tableName, ITableEntity row)
		{
			//get the table we want to save to
			CloudTable tbl = GetTable(tableName);

			//create the operation for the row
			TableOperation op = TableOperation.InsertOrReplace(row);

			//execute the operation
			var res = tbl.Execute(op);
		}

		public TableResult Retrieve(string table, string rowKey, string partitionKey)
		{
			//get the table reference
			CloudTable tbl = GetTable(table);

			//set up a retrieve table operation
			var op = TableOperation.Retrieve(partitionKey, rowKey);

			var data = tbl.Execute(op);


			return data;
		}

		public T Retrieve<T>(string table, string rowKey, string partitionKey)
		{
			//get the table data from the storage table
			var tblRes = Retrieve(table, rowKey, partitionKey);

			//get the type return object
			Type type = typeof(T);

			//get a instance of the return object
			var retObj = Activator.CreateInstance(type);

			var dteResult = (Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity)(tblRes.Result);

			//set the properties of the TableEntity interface first
			type.GetProperty("ETag").SetValue(retObj, dteResult.ETag);
			type.GetProperty("PartitionKey").SetValue(retObj, dteResult.PartitionKey);
			type.GetProperty("RowKey").SetValue(retObj, dteResult.RowKey);
			type.GetProperty("Timestamp").SetValue(retObj, dteResult.Timestamp);

			//iterate througn the storage data and handle each property
			foreach (var prop in dteResult.Properties)
			{
				//use reflection to get the info about the return object property
				var pInfo = type.GetProperty(prop.Key);

				//make sure the property exists in the object
				if (pInfo != null)
				{
					//switch based on the type of the property and set the object value with reflection
					switch (prop.Value.PropertyType)
					{
						case EdmType.Int32:
							pInfo.SetValue(retObj, prop.Value.Int32Value);
							break;
						case EdmType.Int64:
							pInfo.SetValue(retObj, prop.Value.Int64Value);
							break;
						case EdmType.String:
							pInfo.SetValue(retObj, prop.Value.StringValue);
							break;
						case EdmType.Guid:
							pInfo.SetValue(retObj, prop.Value.GuidValue);
							break;
						case EdmType.Boolean:
							pInfo.SetValue(retObj, prop.Value.BooleanValue);
							break;
						case EdmType.DateTime:
                            if (pInfo.PropertyType.Name == "DateTime")
                            {
                                pInfo.SetValue(retObj, prop.Value.DateTime);
                            }
                            else
                            {
                                pInfo.SetValue(retObj, prop.Value.DateTimeOffsetValue);
                            }
							break;
						case EdmType.Double:
							pInfo.SetValue(retObj, prop.Value.DoubleValue);
							break;
						case EdmType.Binary:
							pInfo.SetValue(retObj, prop.Value.BinaryValue);
							break;
					}
				}
			}

			//return the return object
			return (T)retObj;
		}
	}
}
