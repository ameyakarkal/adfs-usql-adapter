using System.Collections.Generic;

namespace Adapter.Sql
{
    public class TableSchema
    {
        public TableSchema(string schemaName, string tableName)
        {
            SchemaName = schemaName;
            TableName = tableName;
            Columns = new List<ColumnSchema>();
        }
        public string SchemaName { get; }
        public string TableName { get; }

        public IList<ColumnSchema> Columns { get; set; }

        public class ColumnSchema
        {
            public string Name { get; set; }

            public string DataType { get; set; }

            public bool IsNullable { get; set; }

        }
    }
}