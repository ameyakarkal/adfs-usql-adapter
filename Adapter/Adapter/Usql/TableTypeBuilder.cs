using System.Linq;
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
            var columns = tableSchema.Columns.Select(col =>
            {
                var nullable = col.IsNullable ? "?" : string.Empty;
                var dataType = Datatype();
                var columnName = ColumnName(col.ColumnName);
                return $"{columnName} {dataType}{nullable}";
            }).ToList();

            var columnDefinitions = string.Join(",\n", columns);

            var catalog = Catalog(tableSchema.Catalog);
            var schema = Schema(tableSchema.Schema);
            var tableTypeName = TableTypeName(tableSchema.TableName);

            var script = $@"CREATE TABLE TYPE [${catalog}].[{schema}].[{tableTypeName}] AS TABLE(
            {columnDefinitions}
            )";

            return new TableType
            {
                Catalog = catalog,
                Schema = schema,
                TableTypeName = tableTypeName,
                Script = script
            };
        }

        public string Catalog(string database)
        {
            return _context.DatabaseName ?? database;
        }

        public string Schema(string schema)
        {
            return _context.Schema ?? schema;
        }

        public string TableTypeName(string tableName)
        {
            return tableName;
        }

        public string ColumnName(string columnName)
        {
            return columnName;
        }

        public string ScriptName(string tableName)
        {
            return $"{TableTypeName(tableName)}TableType.usql";
        }

        public string Datatype()
        {
            return null;
        }

    }
}
