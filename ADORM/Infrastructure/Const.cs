using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

public class Const
{
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
    public class Schema
    {
        public static readonly Dictionary<string, DbProviderFactory> Factories = new Dictionary<string, DbProviderFactory>()
        {
            ["Sql"] = System.Data.SqlClient.SqlClientFactory.Instance,
            ["OleDb"] = System.Data.OleDb.OleDbFactory.Instance,
            ["MySql"] = new MySql.Data.MySqlClient.MySqlClientFactory(),
            ["Oracle"] = new Oracle.ManagedDataAccess.Client.OracleClientFactory(),
            //["SQLite"] = new System.Data.SQLite.SQLiteFactory(),
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
    public class SystemType
    {
        public static readonly Dictionary<Type, string> MapAlias = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(byte[]), "byte[]" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(void), "object" }
        };

        public static readonly Dictionary<Type, DbType> MapDbTypes = new Dictionary<Type, DbType>
        {
            [typeof(byte)] = DbType.Byte,
            [typeof(sbyte)] = DbType.SByte,
            [typeof(short)] = DbType.Int16,
            [typeof(ushort)] = DbType.UInt16,
            [typeof(int)] = DbType.Int32,
            [typeof(uint)] = DbType.UInt32,
            [typeof(long)] = DbType.Int64,
            [typeof(ulong)] = DbType.UInt64,
            [typeof(float)] = DbType.Single,
            [typeof(double)] = DbType.Double,
            [typeof(decimal)] = DbType.Decimal,
            [typeof(bool)] = DbType.Boolean,
            [typeof(string)] = DbType.String,
            [typeof(char)] = DbType.StringFixedLength,
            [typeof(Guid)] = DbType.Guid,
            [typeof(DateTime)] = DbType.DateTime,
            [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
            [typeof(TimeSpan)] = DbType.Time,
            [typeof(byte[])] = DbType.Binary,
            [typeof(byte?)] = DbType.Byte,
            [typeof(sbyte?)] = DbType.SByte,
            [typeof(short?)] = DbType.Int16,
            [typeof(ushort?)] = DbType.UInt16,
            [typeof(int?)] = DbType.Int32,
            [typeof(uint?)] = DbType.UInt32,
            [typeof(long?)] = DbType.Int64,
            [typeof(ulong?)] = DbType.UInt64,
            [typeof(float?)] = DbType.Single,
            [typeof(double?)] = DbType.Double,
            [typeof(decimal?)] = DbType.Decimal,
            [typeof(bool?)] = DbType.Boolean,
            [typeof(char?)] = DbType.StringFixedLength,
            [typeof(Guid?)] = DbType.Guid,
            [typeof(DateTime?)] = DbType.DateTime,
            [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
            [typeof(TimeSpan?)] = DbType.Time,
            [typeof(object)] = DbType.Object
        };
    }
}

