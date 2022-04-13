namespace ADORM.Domain.Objects.Template
{
    public class DatabaseTemplate : BaseTemplate
    {
        public const string ClassName = "Database";

        public override string NameSpace => $"{Context.NameSpaceRoot}.DAL";

        public override string FileName => $"{ClassName}.cs";

        public override string FileContent => @"
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Transactions;

[SuppressMessage(""Security"", ""CA2100"")]
public class Database
{
    protected const string DEFAULTUSERID = ""timetrackpro"";
    protected const string DEFAULTPASSWORD = ""tp2000"";

    public string ProviderName { get; set; }
    public string ConnectionString { get; set; }

    public Database() { }
    public Database(string name)
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
        ProviderName = settings.ProviderName;
        ConnectionString = settings.ConnectionString;
    }
    public static Database Default
    {
        get
        {
            string defaultName = ConfigurationManager.AppSettings.Get(""DefaultConnection"");
            if (!string.IsNullOrWhiteSpace(defaultName))
            {
                return new Database(defaultName);
            }
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1];
            return new Database
            {
                ProviderName = settings.ProviderName,
                ConnectionString = settings.ConnectionString
            };
        }
    }

    public DbProviderFactory GetProviderFactory()
    {
        return DbProviderFactories.GetFactory(ProviderName);
    }
    public DbConnection CreateConnection()
    {
        DbProviderFactory factory = GetProviderFactory();
        DbConnection conn = factory.CreateConnection();
        conn.ConnectionString = Regex.Replace(Regex.Replace(ConnectionString, nameof(DEFAULTUSERID), DEFAULTUSERID, RegexOptions.IgnoreCase), nameof(DEFAULTPASSWORD), DEFAULTPASSWORD, RegexOptions.IgnoreCase);
        return conn;
    }
    public DbConnection OpenConnection()
    {
        DbConnection conn = CreateConnection();
        conn.Open();
        return conn;
    }
    public DbCommand CreateCommand()
    {
        DbProviderFactory factory = GetProviderFactory();
        DbCommand dbCmd = factory.CreateCommand();
        dbCmd.CommandTimeout = int.TryParse(ConfigurationManager.AppSettings.Get(""DbCommandTimeout""), out int timeout) ? timeout : 180;
        return dbCmd;
    }
    public DbParameter CreateParameter(string name, object value, DbType dbType, int size = 0, ParameterDirection direction = ParameterDirection.Input)
    {
        DbProviderFactory factory = GetProviderFactory();
        DbParameter para = factory.CreateParameter();
        para.Direction = direction;
        para.ParameterName = name;
        para.Value = value ?? DBNull.Value;
        para.DbType = dbType;
        para.Size = size;
        return para;
    }
    public DbCommand GetSqlStringCommand(string sql, DbConnection conn = null)
    {
        DbCommand dbCmd = CreateCommand();
        dbCmd.CommandType = CommandType.Text;
        dbCmd.CommandText = sql;
        dbCmd.Connection = conn;
        return dbCmd;
    }
    public DbCommand GetSqlStringCommand(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = CreateCommand();
        dbCmd.CommandText = sql;
        if (parameters != null && parameters.Length > 0)
        {
            dbCmd.Parameters.AddRange(parameters);
        }
        return dbCmd;
    }
    public DbCommand GetStoredProcCommand(string procedureName, DbConnection conn = null)
    {
        DbCommand dbCmd = CreateCommand();
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.CommandText = procedureName;
        dbCmd.Connection = conn;
        return dbCmd;
    }
    public DbParameter AddParameter(DbCommand dbCmd, string name, object value, DbType dbType, int size, ParameterDirection direction)
    {
        DbParameter para = CreateParameter(name, value, dbType, size, direction);
        dbCmd.Parameters.Add(para);
        return para;
    }
    public void AddInParameter(DbCommand dbCmd, string name, object value, DbType dbType, int size = 0)
    {
        AddParameter(dbCmd, name, value, dbType, size, ParameterDirection.Input);
    }
    public DbParameter AddOutParameter(DbCommand dbCmd, string name, DbType dbType, int size = 0)
    {
        return AddParameter(dbCmd, name, null, dbType, size, ParameterDirection.Output);
    }
    public int ExecuteNonQuery(DbCommand dbCmd, DbTransaction tran = null)
    {
        using (new CommandWrapper(this, dbCmd, tran))
        {
            return dbCmd.ExecuteNonQuery();
        }
    }
    public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteNonQuery(dbCmd);
    }
    public DbDataReader ExecuteReader(DbCommand dbCmd)
    {
        using (new CommandWrapper(this, dbCmd, closeConn: false))
        {
            return dbCmd.ExecuteReader();
        }
    }
    public DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteReader(dbCmd);
    }
    public DbDataReader ExecuteReader(DbCommand dbCmd, CommandBehavior behavior)
    {
        using (new CommandWrapper(this, dbCmd))
        {
            return dbCmd.ExecuteReader(behavior);
        }
    }
    public bool Exists(string sql, params DbParameter[] parameters)
    {
        using (DbDataReader reader = ExecuteReader(sql, parameters))
        {
            return reader.Read();
        }
    }
    public object ExecuteScalar(DbCommand dbCmd)
    {
        using (new CommandWrapper(this, dbCmd))
        {
            return dbCmd.ExecuteScalar();
        }
    }
    public object ExecuteScalar(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteScalar(dbCmd);
    }
    public T ExecuteScalar<T>(DbCommand dbCmd)
    {
        object scalar = ExecuteScalar(dbCmd);
        if (scalar != null && scalar != DBNull.Value)
        {
            if (scalar is T equal)
                return equal;
            else
                return (T)Convert.ChangeType(scalar, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
        }
        return default(T);
    }
    public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteScalar<T>(dbCmd);
    }
    public DataSet ExecuteDataSet(DbCommand dbCmd)
    {
        using (new CommandWrapper(this, dbCmd))
        {
            DbProviderFactory factory = GetProviderFactory();
            DbDataAdapter dbAdapter = factory.CreateDataAdapter();
            DataSet ds = new DataSet();
            dbAdapter.SelectCommand = dbCmd;
            dbAdapter.Fill(ds);
            return ds;
        }
    }
    public DataSet ExecuteDataSet(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteDataSet(dbCmd);
    }
    public DataTable ExecuteDataTable(DbCommand dbCmd, int tableIndex = 0)
    {
        DataSet ds = ExecuteDataSet(dbCmd);
        if (ds != null && ds.Tables != null && ds.Tables.Count > tableIndex)
        {
            return ds.Tables[tableIndex];
        }
        return null;
    }
    public DataTable ExecuteDataTable(string sql, params DbParameter[] parameters)
    {
        DbCommand dbCmd = GetSqlStringCommand(sql, parameters);
        return ExecuteDataTable(dbCmd);
    }
    public DataTable ExecutePagedTable(DbCommand dbCmd, out int total, string orderBy, int pageNum = 1, int pageSize = 10)
    {
        dbCmd.CommandText = $@""
SELECT *
     , ROW_NUMBER() OVER (ORDER BY {orderBy}) rn
  INTO #__TEMP
  FROM (
      {dbCmd.CommandText}
  ) __TEMP;

SELECT @__total = COUNT(1)
  FROM #__TEMP;

SELECT *
  FROM #__TEMP
 WHERE rn BETWEEN @__begin AND @__end;
"";
        AddInParameter(dbCmd, ""__begin"", (pageNum - 1) * pageSize + 1, DbType.Int32);
        AddInParameter(dbCmd, ""__end"", pageNum * pageSize, DbType.Int32);
        DbParameter paraTotal = AddOutParameter(dbCmd, ""__total"", DbType.Int32);
        DataTable table = ExecuteDataTable(dbCmd);
        total = paraTotal.Value is int ttl ? ttl : 0;
        return table;
    }
    public class CommandWrapper : IDisposable
    {

        private readonly DbCommand _dbCmd;
        private readonly bool _needClose;
        public CommandWrapper(Database db, DbCommand dbCmd, DbTransaction tran = null, bool? closeConn = null)
        {
            _dbCmd = dbCmd;
            if (tran != null)
            {
                _dbCmd.Transaction = tran;
                _dbCmd.Connection = tran.Connection;
            }
            else if (_dbCmd.Connection == null)
            {
                if (Transaction.Current != null)
                {
                    _dbCmd.Connection = TransactionScopeConnections.GetTransactionConnection(db);
                }
                else
                {
                    _dbCmd.Connection = db.CreateConnection();
                    _needClose = true;
                }
            }
            if (_dbCmd.Connection.State == ConnectionState.Closed)
            {
                _dbCmd.Connection.Open();
            }
            if (closeConn.HasValue)
            {
                _needClose = closeConn.Value;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_needClose)
                {
                    _dbCmd.Connection.Dispose();
                }
                _dbCmd.Parameters.Clear();
                // ...
            }
            // ...
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public class TransactionScopeConnections
    {
        private static readonly Dictionary<string, DbConnection> _transactionConncetionPool = new Dictionary<string, DbConnection>();
        public static DbConnection GetTransactionConnection(Database db)
        {
            if (Transaction.Current == null) return null;
            string id = Transaction.Current.TransactionInformation.LocalIdentifier;
            lock (_transactionConncetionPool)
            {
                if (!_transactionConncetionPool.ContainsKey(id) || _transactionConncetionPool[id] == null)
                {
                    _transactionConncetionPool[id] = db.CreateConnection();
                    Transaction.Current.TransactionCompleted += Current_TransactionCompleted;
                }
                return _transactionConncetionPool[id];
            }
        }
        private static void Current_TransactionCompleted(object sender, TransactionEventArgs e)
        {
            string id = e.Transaction.TransactionInformation.LocalIdentifier;
            lock (_transactionConncetionPool)
            {
                if (_transactionConncetionPool.ContainsKey(id))
                {
                    _transactionConncetionPool[id]?.Dispose();
                    _transactionConncetionPool.Remove(id);
                }
            }
        }
    }
}";
    }
}
