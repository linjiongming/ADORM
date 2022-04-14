using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADORM.Domain.Objects.Schema
{
    public class ColumnSchema
    {
        public string Name { get; set; }
        public Type SystemType { get; set; }
        public DbType DataType => Const.SystemType.MapDbTypes.TryGetValue(SystemType, out var dbType)? dbType: DbType.Object;
        public bool IsNullable { get; set; }
        public bool PrimaryKey { get; set; }
        public bool IsIdentity { get; set; }
        public int Length { get; set; }

        private string _typeName;
        
        public string TypeName => _typeName ?? (_typeName = TypeNameOrAlias(SystemType) + (IsNullable && SystemType.IsValueType ? "?" : ""));

        public static string TypeNameOrAlias(Type type)
        {
            if (Const.SystemType.MapAlias.TryGetValue(type, out string alias))
                return alias;
            return type.Name;
        }        
    }
}
