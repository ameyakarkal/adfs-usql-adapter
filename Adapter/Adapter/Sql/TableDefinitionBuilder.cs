using System;
using System.Linq;
using Dapper;

namespace Adapter.Sql
{
    public class TableDefinitionBuilder
    {
        private readonly DataSource _conn;
        private const string TableDefinition = @"
        SELECT
        TABLE_CATALOG AS Catalog,
        TABLE_SCHEMA AS Schema,
        TABLE_NAME AS Table
        
        FROM information_schema.[tables]
        WHERE 
            TABLE_TYPE='BASETABLE'
        AND Table_NAME = @tableName";

        private const string ColumnDefinition = @"
        SELECT
        column_name AS  ColumnName,
        data_type AS DataType,
        case when is_nullable = 'NO' then 0 else 1 end as IsNullable

        FROM information_schema.columns
        WHERE 
            table_schema = @tableSchema
        and table_name = @tableName
        ORDER BY 
        ordinal_position";

        public TableDefinitionBuilder(DataSource conn)
        {
            _conn = conn;
        }

        public TableSchema GetTableSchema(string tableSchema, string tableName)
        {
            using (var conn = _conn.GetConnection())
            {
                conn.Open();
                var table = conn.Query<TableSchema>(TableDefinition, new {tableSchema, tableName}).FirstOrDefault();

                if(table == null)
                    throw new ArgumentException($"Table {tableSchema}.{tableName} not found");

                table.Columns = conn.Query<TableSchema.ColumnSchema>(ColumnDefinition, new { tableSchema, tableName }).ToList();

                return table;
            }
        }

    }
}