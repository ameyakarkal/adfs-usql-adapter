using System;
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
                var dataType = Datatype(col.DataType, col.IsNullable);
                var columnName = ColumnName(col.ColumnName);
                return $"{columnName} {dataType}";
            }).ToList();

            var columnDefinitions = string.Join(",\n", columns);

            var catalog = Catalog(tableSchema.Catalog);
            var schema = Schema(tableSchema.Schema);
            var tableTypeName = TableTypeName(tableSchema.TableName);
            var scriptName = tableTypeName + ".TableType.usql";
            var script = $@"CREATE TABLE TYPE [${catalog}].[{schema}].[{tableTypeName}] AS TABLE(
{columnDefinitions}
)";
            return new TableType
            {
                Catalog = catalog,
                Schema = schema,
                TableTypeName = tableTypeName,
                Script = script,
                ScriptName = scriptName
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

        //add more cases
        public string Datatype(string datatype, bool isNullable)
        {
            string dataType;
            switch (datatype.ToLower())
            {
                case "bit":
                    dataType = "bool";
                    break;
                case "int":
                    dataType = "int";
                    break;
                case "datetime":
                case "datetimeoffset":
                    dataType = "DateTime";
                    break;
                case "nvarchar":
                case "varchar":
                    dataType = "string";
                    break;
                case "uniqueidentifier":
                    dataType = "Guid";
                    break;
                default:
                    throw new Exception($"{datatype} not mapped");
            }
            if (isNullable 
                && "string" != dataType)
            {
                dataType += "?";
            }

            return dataType;
        }

    }
}
