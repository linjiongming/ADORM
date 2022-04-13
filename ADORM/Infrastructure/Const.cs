using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public class Const
{
    public class Schema
    {
        public static readonly Dictionary<string, DbProviderFactory> Factories = new Dictionary<string, DbProviderFactory>()
        {
            ["Sql"] = System.Data.SqlClient.SqlClientFactory.Instance,
            ["OleDb"] = System.Data.OleDb.OleDbFactory.Instance,
            ["MySql"] = new MySql.Data.MySqlClient.MySqlClientFactory(),
            ["Oracle"] = new Oracle.ManagedDataAccess.Client.OracleClientFactory(),
            ["SQLite"] = new System.Data.SQLite.SQLiteFactory(),
        };

        private static string _description;
        public static string Description => _description ?? (_description = $"\r\n\tDatabase Providers:{string.Concat(Factories.Select(x => $"\r\n\t\t{x.Key}:\t{x.Value.GetType().Namespace}"))}");

        public static DbProviderFactory GetFactory(string key)
        {
            key = Factories.Keys.FirstOrDefault(x => x.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(key))
            {
                return Factories[key];
            }
            return null;
        }
    }
    public class Cli
    {
        public static readonly string SchemaProviderDesc;
        public static readonly string HelpText;

        static Cli()
        {
            SchemaProviderDesc = string.Concat(Schema.Factories.Select(x => $"\r\n\t\t{x.Key}{(x.Key.Length > 7 ? "\t" : "\t\t")}{x.Value.GetType().Namespace}"));
            HelpText = $@"
Options:

    -ns         namespace root          [Default:My]

    -conn       connection string       [Mandatory]

    -provider   schema provider         [Default:sql]
{SchemaProviderDesc}

    -t          select tables           [split by comma(,)]
";
        }
    }
}

