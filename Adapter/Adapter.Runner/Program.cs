using System;
using Adapter.Sql;
using Adapter.Usql;
using Microsoft.Extensions.Configuration;

namespace Adapter.Runner
{
    class Program
    {

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            //what the run needs
            var connString = configuration.GetConnectionString("database");

            var schema = configuration["schema"];

            var table = configuration["table"];

            //run
            var tableSchema =  new TableDefinitionBuilder(new DataSource(connString)).GetTableSchema(schema, table);

            var typeType = new TableTypeBuilder().Build(tableSchema);

            new Writer("output", typeType).Write();

            Console.Write("scripts generated");
            Console.ReadLine();
        }
    }
}
