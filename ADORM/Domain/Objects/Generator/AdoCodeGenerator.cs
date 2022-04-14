using ADORM.Domain.Objects.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ADORM.Domain.Contexts;
using ADORM.Domain.Objects.Schema;
using System.Data.Common;

namespace ADORM.Domain.Objects.Generator
{
    public class AdoCodeGenerator : IDisposable
    {
        private readonly GeneratorOption _option;
        private readonly EscapeSymbol _escapeSymbol;
        private readonly DbDataAdapter _dataAdapter;
        private readonly DbConnection _connection;
        private readonly object _cursorLocker = new object();

        public event Action<int, object> OnReportProgress;

        public TableSchema[] Tables { get; private set; }
        public TemplateContext Context { get; private set; }
        public bool IsRunning { get; private set; }
        public CancellationTokenSource Cancellation { get; private set; }
        public List<Task[]> TaskBus { get; set; }
        public int TaskCount => TaskBus.Sum(x => x.Length);
        public int TaskCursor { get; set; }

        public AdoCodeGenerator(GeneratorOption option)
        {
            _option = option;
            _escapeSymbol = EscapeSymbol.FromProviderFactory(_option.DbProviderFactory);
            _dataAdapter = _option.DbProviderFactory.CreateDataAdapter();
            {
                _dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            }
            _connection = _option.DbProviderFactory.CreateConnection();
            {
                _connection.ConnectionString = _option.ConnectionString;
                _connection.Open();
            }
            if (!string.IsNullOrWhiteSpace(option.Tables))
            {
                SelectTables(option.Tables);
            }
            Logger.Instance.OnLogInfo += Logger_WriteLog;
            Logger.Instance.OnLogError += Logger_WriteLog;
        }

        private void Logger_WriteLog(object log)
        {
            OnReportProgress?.Invoke(TaskCursor, log);
        }

        public IEnumerable<string> GetAllTableNames()
        {
            var dataTable = _connection.GetSchema("Tables");
            foreach (DataRow row in dataTable.Rows)
            {
                yield return row.Field<string>(2);
            }
        }

        public void SelectTables(string tables)
        {
            SelectTables(tables.Split(','));
        }

        public void SelectTables(IEnumerable<string> tables)
        {
            Tables = tables.Select(GetTableSchema).ToArray();
        }

        public TableSchema GetTableSchema(string tableName)
        {
            var escapeTableName = _escapeSymbol.Escape(tableName);
            Logger.Info($"The schema of table {escapeTableName} is loading...");
            var tableSchema = new TableSchema(tableName);
            var dataTable = new DataTable();
            var command = _option.DbProviderFactory.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"SELECT * FROM {escapeTableName}";
            _dataAdapter.SelectCommand = command;
            _dataAdapter.FillSchema(dataTable, SchemaType.Source);
            foreach (DataColumn col in dataTable.Columns)
            {
                tableSchema.Columns.Add(new ColumnSchema
                {
                    Name = col.ColumnName,
                    SystemType = col.DataType,
                    IsIdentity = col.AutoIncrement,
                    IsNullable = col.AllowDBNull,
                    Length = col.MaxLength,
                    PrimaryKey = dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length > 0 ? dataTable.PrimaryKey.Contains(col) : (col.AutoIncrement || col.Unique),
                });
            }
            return tableSchema;
        }

        private Task NewTask(Action action)
        {
            return new Task(delegate
            {
                if (!Cancellation.Token.IsCancellationRequested)
                {
                    try
                    {
                        action();
                        lock (_cursorLocker)
                        {
                            TaskCursor++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        Cancellation.Cancel();
                    }
                }
            }, Cancellation.Token);
        }

        public AdoCodeGenerator Ready()
        {
            TaskCursor = 0;
            TaskBus = new List<Task[]>();
            Cancellation = new CancellationTokenSource();
            Context = new TemplateContext(_option.NameSpace);
            var initTasks = Context.LoadEntities(Tables).Select(NewTask).ToArray();
            {
                TaskBus.Add(initTasks);
            }
            EntityProjectTemplate entityProject = null;
            if (_option.GenerateEntities)
            {
                entityProject = Context.Add(new EntityProjectTemplate());
                Context.Add(new EntityAssemblyInfoTemplate(entityProject));
                //Context.Add(new BaseEntityTemplate());
                Context.AddByIndexes(EntityTemplate.FromIndex);
            }
            ReposProjectTemplate reposProject = null;
            if (_option.GenerateRepositories)
            {
                reposProject = Context.Add(new ReposProjectTemplate(entityProject));
                Context.Add(new ReposAssemblyInfoTemplate(reposProject));
                Context.Add(new DatabaseTemplate());
                Context.Add(new BaseReposTemplate());
                Context.AddByIndexes(ReposTemplate.FromIndex);
            }
            var genTasks = Context.Generate().Select(NewTask).ToArray();
            {
                TaskBus.Add(genTasks);
            }
            return this;
        }

        public void Go()
        {
            IsRunning = true;
            try
            {
                foreach (var tasks in TaskBus)
                {
                    tasks.ForEach(x => x.Start());
                    Task.WaitAll(tasks);
                }
                Logger.Info("All codes have been generated successfully.");
                System.Diagnostics.Process.Start("explorer.exe", Context.OutputFolder);
            }
            catch (AggregateException aex)
            {
                if(aex.InnerException is TaskCanceledException)
                {
                    Logger.Info("Cancelled!");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                IsRunning = false;
            }
        }

        public void Cancel()
        {
            Cancellation.Cancel();
            TaskCursor = 0;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }

    public class EscapeSymbol
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public EscapeSymbol(string left, string right = null)
        {
            Left = left;
            Right = right ?? left;
        }
        public static EscapeSymbol FromProviderFactory(DbProviderFactory providerFactory)
        {
            if (providerFactory.GetType().Name.Contains("MySql"))
            {
                return new EscapeSymbol("`");
            }
            else
            {
                return new EscapeSymbol("[", "]");
            }
        }
        public string Escape(string schemaName)
        {
            return Left + schemaName.TrimStart(Left.ToCharArray()).TrimEnd(Right.ToCharArray()) + Right;
        }
    }
}
