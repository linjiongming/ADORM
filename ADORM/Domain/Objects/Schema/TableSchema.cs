using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADORM.Domain.Objects.Schema
{
    public class TableSchema
    {
        public TableSchema(string name)
        {
            Name = name;
            Columns = new List<ColumnSchema>();
        }

        public string Name { get; set; }

        public List<ColumnSchema> Columns { get; set; }

        public List<ColumnSchema> PrimaryKeys => Columns.FindAll(x => x.PrimaryKey);

        private string _pascalName;
        public string PascalName => _pascalName ?? (_pascalName = GetPascalName(Name));

        public static readonly string[] TablePrefixes = new[] { "t_", "tb_", "tbl_", "tbl" };

        public static string GetPascalName(string tableName)
        {
            var entityName = tableName;
            var prefix = TablePrefixes.FirstOrDefault(x => entityName.StartsWith(x, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrWhiteSpace(prefix) && entityName.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase))
            {
                entityName = entityName.Substring(prefix.Length);
            }
            return string.Concat(entityName.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1)));
        }
    }
}
