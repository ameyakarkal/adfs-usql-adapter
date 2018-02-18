using System.Collections.Generic;

namespace Adapter.Sql
{
    public class TableSchema
    {
        public string Catalog { get; set; }

        public string Schema { get; set; }

        public string TableName { get; set; }

        public IList<ColumnSchema> Columns { get; set; }

        public class ColumnSchema
        {
            public string ColumnName { get; set; }

            public string DataType { get; set; }

            public bool IsNullable { get; set; }

        }
    }
}