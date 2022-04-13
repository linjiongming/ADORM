using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ADORM.Domain.Objects.Schema
{
    public class EntitySchema
    {
        public static string[] TablePrefixes = new[] { "t_", "tb_" };
        public const string ColumnPrefix = "c_";

        public string TableName { get; set; }
        public string Name { get; set; }
        public string PascalName { get; set; }
        public List<PropertySchema> Properties { get; set; }
        public string Description { get; set; }

        public EntitySchema(TableSchema table)
        {
            TableName = table.Name;
            Name = table.Name;
            PascalName = table.PascalName;
            //Description = string.IsNullOrWhiteSpace(table.Description) ? table.FullName : table.Description.Replace("\n", string.Empty);
            Description = table.Name;
            Properties = table.Columns.Select(x => new PropertySchema
            {
                ColumnName = x.Name,
                Name = x.Name,
                //Description = string.IsNullOrWhiteSpace(x.Description) ? x.Name : x.Description.Replace("\n", string.Empty),
                Description = x.Name,
                DataType = x.DataType,
                TypeName = x.TypeName,
                IsNullable = x.IsNullable,
                IsIdentity = x.IsIdentity,
                IsPrimaryKey = x.PrimaryKey,
                Length = x.Length,
            }).ToList();
        }

        //private bool IsIdentity(ColumnSchema column)
        //{
        //    if (column.ExtendedProperties.Contains("CS_IsIdentity") && column.ExtendedProperties["CS_IsIdentity"].Value != null)
        //    {
        //        return (bool)column.ExtendedProperties["CS_IsIdentity"].Value;
        //    }
        //    return false;
        //}

        //private string GetTypeName(ColumnSchema column)
        //{
        //    string typeName;
        //    switch (column.DataType)
        //    {
        //        case DbType.AnsiString:
        //            typeName = "string";
        //            break;
        //        case DbType.AnsiStringFixedLength:
        //            if (column.Size == 36)
        //            {
        //                typeName = "Guid";
        //            }
        //            else
        //            {
        //                typeName = "string";
        //            }
        //            break;
        //        case DbType.Binary:
        //            typeName = "byte[]";
        //            break;
        //        case DbType.Boolean:
        //            typeName = "bool";
        //            break;
        //        case DbType.Byte:
        //            typeName = "byte";
        //            break;
        //        case DbType.Currency:
        //            typeName = "decimal";
        //            break;
        //        case DbType.Date:
        //            typeName = "DateTime";
        //            break;
        //        case DbType.DateTime:
        //            typeName = "DateTime";
        //            break;
        //        case DbType.Decimal:
        //            typeName = "decimal";
        //            break;
        //        case DbType.Double:
        //            typeName = "double";
        //            break;
        //        case DbType.Guid:
        //            typeName = "Guid";
        //            break;
        //        case DbType.Int16:
        //            typeName = "short";
        //            break;
        //        case DbType.Int32:
        //            typeName = "int";
        //            break;
        //        case DbType.Int64:
        //            typeName = "long";
        //            break;
        //        case DbType.Object:
        //            typeName = "object";
        //            break;
        //        case DbType.SByte:
        //            typeName = "sbyte";
        //            break;
        //        case DbType.Single:
        //            typeName = "float";
        //            break;
        //        case DbType.String:
        //            typeName = "string";
        //            break;
        //        case DbType.StringFixedLength:
        //            if (column.Size == 36)
        //            {
        //                typeName = "Guid";
        //            }
        //            else
        //            {
        //                typeName = "string";
        //            }
        //            break;
        //        case DbType.Time:
        //            typeName = "TimeSpan";
        //            break;
        //        case DbType.UInt16:
        //            typeName = "ushort";
        //            break;
        //        case DbType.UInt32:
        //            typeName = "uint";
        //            break;
        //        case DbType.UInt64:
        //            typeName = "ulong";
        //            break;
        //        case DbType.VarNumeric:
        //            if (column.Scale == 0)
        //            {
        //                if (column.Precision > 9)
        //                {
        //                    typeName = "long";
        //                }
        //                else
        //                {
        //                    typeName = "int";
        //                }
        //            }
        //            else
        //            {
        //                typeName = "double";
        //            }
        //            break;
        //        default:
        //            typeName = "string";
        //            break;
        //    }
        //    if (column.AllowDBNull && typeName != "string" && typeName != "byte[]")
        //    {
        //        typeName += "?";
        //    }
        //    return typeName;
        //}

        //public static string GetEntityName(string tableName)
        //{
        //    var entityName = tableName;
        //    foreach (var prefix in TablePrefixes)
        //    {
        //        if (entityName.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            entityName = entityName.Substring(prefix.Length);
        //            break;
        //        }
        //    }
        //    return SnakeCaseToPascalCase(entityName);
        //}
        //public static string GetPropertyName(string columnName)
        //{
        //    var propertyName = columnName;
        //    if (propertyName.StartsWith(ColumnPrefix, StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        propertyName = propertyName.Substring(ColumnPrefix.Length);
        //    }
        //    else
        //    {
        //        var first = columnName[0];
        //        var second = columnName[1];
        //        if (second >= 'A' && second <= 'Z' && new[] { 'n', 's', 'b' }.Contains(first))
        //        {
        //            propertyName = propertyName.Substring(1);
        //        }
        //    }
        //    return SnakeCaseToPascalCase(propertyName);
        //}
        //public static string SnakeCaseToPascalCase(string snakeCase)
        //{
        //    return string.Concat(snakeCase.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1)));
        //}
    }
}
