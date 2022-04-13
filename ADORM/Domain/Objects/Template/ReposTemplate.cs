using ADORM.Domain.Objects.Schema;
using System.Linq;

namespace ADORM.Domain.Objects.Template
{
    public class ReposTemplate : BaseTemplate
    {
        public const string ParamPrefix = "@";
        public const string FolderName = "DAL";
        public const string Suffix = "Provider";

        private readonly int _entityIndex;

        public ReposTemplate(int entityIndex)
        {
            _entityIndex = entityIndex;
        }

        public static ReposTemplate FromIndex(int entityIndex)
        {
            return new ReposTemplate(entityIndex);
        }

        public EntitySchema Entity => Context.Entities[_entityIndex];

        public override string NameSpace => $"{Context.NameSpaceRoot}.{FolderName}";

        public override string FileName => $"{Entity.PascalName}{Suffix}.cs";

        public override string FileContent => $@"{Profile}
using {Context.NameSpaceRoot}.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;

namespace {NameSpace}
{{
    /// <summary>
    /// [Repository] {Entity.Description}
    /// </summary>
    public partial class {Entity.PascalName}{Suffix} : {BaseReposTemplate.ClassName}
    {{
        public static {Entity.Name} FromRow(DataRow row)
        {{
            {Entity.Name} entity = new {Entity.Name}();
            {string.Join("\r\n            ", Entity.Properties.Select(x => $"if (row.Table.Columns.Contains(\"{x.Name}\") && !row.IsNull(\"{x.Name}\")) entity.{x.Name} = row.Field<{x.TypeName.TrimEnd('?')}>(\"{x.Name}\");"))}
            return entity;
        }}
        public static List<{Entity.Name}> FromTable(DataTable dt)
        {{
            List<{Entity.Name}> list = new List<{Entity.Name}>();
            foreach (DataRow dr in dt.Rows)
            {{
                list.Add(FromRow(dr));
            }}
            return list;
        }}
        public int Insert({Entity.Name} entity)
        {{
            string sql = @""{InsertSql}"";
            DbCommand dbCmd = DB.GetSqlStringCommand(sql);
            {string.Join("\r\n            ", InsertCols.Select(x => $"DB.AddInParameter(dbCmd, \"{x.Name}\", entity.{x.Name}, DbType.{x.DataType});"))}
            {(Identity == null ? string.Empty : $"DbParameter IdParameter = DB.AddOutParameter(dbCmd, \"{Identity.ColumnName}\", DbType.{Identity.DataType});")}
            int effected = DB.ExecuteNonQuery(dbCmd);
            {(Identity == null ? string.Empty : $@"if (IdParameter.Value != null && IdParameter.Value != DBNull.Value)
                entity.{Identity.Name} = IdParameter.Value is {Identity.UnderType} equal ? equal : ({Identity.UnderType})Convert.ChangeType(IdParameter.Value, typeof({Identity.UnderType}));")}
            return effected;
        }}
        public int Update({Entity.Name} entity)
        {{
            string sql = @""{UpdateSql}"";
            DbCommand dbCmd = DB.GetSqlStringCommand(sql);
            {string.Join("\r\n            ", UpdateCols.Select(x => $"DB.AddInParameter(dbCmd, \"{x.Name}\", entity.{x.Name}, DbType.{x.DataType});"))}
            DB.AddInParameter(dbCmd, ""{Key.ColumnName}"", entity.{Key.Name}, DbType.{Key.DataType});
            return DB.ExecuteNonQuery(dbCmd);
        }}
        public int Delete({Key.TypeName} id)
        {{
            string sql = ""DELETE FROM {Entity.TableName} WHERE {Key.ColumnName} = {ParamPrefix + Key.ColumnName}"";
            DbCommand dbCmd = DB.GetSqlStringCommand(sql);
            DB.AddInParameter(dbCmd, ""{Key.ColumnName}"", id, DbType.{Key.DataType});
            return DB.ExecuteNonQuery(dbCmd);
        }}
        public int Delete(string where, params DbParameter[] parameters)
        {{
            string sql = ""DELETE FROM {Entity.TableName} WHERE "" + where;
            return DB.ExecuteNonQuery(sql, parameters);
        }}
        public {Entity.Name} GetEntity({Key.TypeName} id)
        {{
            string sql = ""SELECT * FROM {Entity.TableName} WHERE {Key.ColumnName} = {ParamPrefix + Key.ColumnName}"";
            DbCommand dbCmd = DB.GetSqlStringCommand(sql);
            DB.AddInParameter(dbCmd, ""{Key.ColumnName}"", id, DbType.{Key.DataType});
            DataTable dt = DB.ExecuteDataTable(dbCmd);
            if (dt != null && dt.Rows.Count > 0)
            {{
                return FromRow(dt.Rows[0]);
            }}
            return null;
        }}
        public List<{Entity.Name}> GetList(string where, params DbParameter[] parameters)
        {{
            string sql = ""SELECT * FROM {Entity.TableName} WHERE "" + where;
            DataTable dt = DB.ExecuteDataTable(sql, parameters);
            return FromTable(dt);
        }}
        public List<{Entity.Name}> GetPagedList(string where, DbParameter[] parameters, out int total, string orderBy = ""{Key.ColumnName}"", int pageNum = 1, int pageSize = 10)
        {{
            DbCommand dbCmd = DB.CreateCommand();
            dbCmd.CommandType = CommandType.Text;
            dbCmd.CommandText = ""SELECT * FROM {Entity.TableName} WHERE "" + where;
            dbCmd.Parameters.AddRange(parameters);
            DataTable dt = DB.ExecutePagedTable(dbCmd, out total, orderBy, pageNum, pageSize);
            return FromTable(dt);
        }}
    }}
}}";

        private PropertySchema _identity;
        public PropertySchema Identity
        {
            get
            {
                if (_identity == null)
                {
                    _identity = Entity.Properties.FirstOrDefault(x => x.IsIdentity);
                }
                return _identity;
            }
        }

        private PropertySchema _primaryKey;
        public PropertySchema PrimaryKey
        {
            get
            {
                if (_primaryKey == null)
                {
                    _primaryKey = Entity.Properties.FirstOrDefault(x => x.IsPrimaryKey);
                }
                return _primaryKey;
            }
        }

        private PropertySchema _key;
        public PropertySchema Key
        {
            get
            {
                if (_key == null)
                {
                    _key = PrimaryKey ?? Identity;
                }
                return _key;
            }
        }

        private PropertySchema[] _insertCols;
        public PropertySchema[] InsertCols
        {
            get
            {
                if (_insertCols == null)
                {
                    _insertCols = Entity.Properties.Where(x => !x.IsIdentity).ToArray();
                }
                return _insertCols;
            }
        }

        private PropertySchema[] _updateCols;
        public PropertySchema[] UpdateCols
        {
            get
            {
                if (_updateCols == null)
                {
                    _updateCols = Entity.Properties.Where(x => !x.IsIdentity && !x.IsPrimaryKey).ToArray();
                }
                return _updateCols;
            }
        }

        public string InsertSql => $@"
INSERT INTO {Entity.TableName} (
    {string.Join("\r\n  , ", InsertCols.Select(x => x.ColumnName))}
)
VALUES (
    {string.Join("\r\n  , ", InsertCols.Select(x => ParamPrefix + x.ColumnName))}
);
{(Identity == null ? string.Empty : $"SELECT @{Identity.ColumnName} = SCOPE_IDENTITY();")}
";

        public string UpdateSql => $@"
UPDATE {Entity.TableName} 
   SET {string.Join("\r\n     , ", UpdateCols.Select(x => $"{x.ColumnName} = {ParamPrefix}{x.ColumnName}"))}
 WHERE {Key.ColumnName} = {ParamPrefix + Key.ColumnName}
";

        public override void Generate()
        {
            if (Identity == null)
            {
                Logger.Info($"Table {Entity.TableName} has no identity column!");
                if (PrimaryKey == null)
                {
                    Logger.Info($"Table {Entity.TableName} has no primary key!!");
                    return;
                }
            }
            base.Generate();
        }
    }
}
