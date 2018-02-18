using Adapter.Sql;

namespace Adapter.Usql
{
    public class TableTypeBuilder
    {
        private readonly UsqlContext _context;

        public TableTypeBuilder()
        {
            _context = new NullUsqlContext();
        }

        public TableTypeBuilder(UsqlContext context)
        {
            _context = context;
        }

        public TableType Build(TableSchema tableSchema)
        {
            return null;
        }

        public string Database(string database)
        {
            return _context.DatabaseName ?? database;
        }

        public string Schema(string schema)
        {
            return _context.Schema ?? schema;
        }

        public string TableName(string tableName)
        {
            return tableName;
        }

        public string ColumnName(string columnName)
        {
            return columnName;
        }

        public string ScriptName(string tableName)
        {
            return $"{TableName(tableName)}TableType.usql";
        }

        public string Datatype()
        {
            return null;
        }

//        public TableType Build(string TableSchema tableSchema)
//        {
//            var columns = tableSchema.Columns.Select(col =>
//            {
//                var nullable = col.IsNullable ? "?" : string.Empty;
//                var dataType = GetDataType(col.DataType);
//                var columnName = GetColumnName(col.Name);
//                return $"{columnName} {dataType}{nullable}";
//            }).ToList();

//            var columnDefinitions = string.Join(",\n", columns);

//            var script = $@"CREATE TABLE TYPE [{usqlDatabase}].[{tableSchema.SchemaName}].[{tableSchema.TableName}] AS TABLE(
//{columnDefinitions}
//)";

//            return new TableType
//            {
//                FileName = $"{tableSchema.TableName}.TableType.usql",
//                Script = script
//            };
//        }


    }
}
