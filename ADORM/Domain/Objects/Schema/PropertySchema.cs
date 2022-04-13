using System;
using System.Data;

namespace ADORM.Domain.Objects.Schema
{
    public class PropertySchema
    {
        public string ColumnName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type DataType { get; set; }
        public string TypeName { get; set; }
        public string UnderType => TypeName.TrimEnd('?');
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimaryKey { get; set; }
        public int Length { get; set; }
    }

}
