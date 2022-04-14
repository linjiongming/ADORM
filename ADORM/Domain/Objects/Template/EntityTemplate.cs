using ADORM.Domain.Objects.Schema;
using System.Linq;

namespace ADORM.Domain.Objects.Template
{
    public class EntityTemplate : BaseTemplate
    {
        private readonly int _entityIndex;

        public EntityTemplate(int entityIndex)
        {
            _entityIndex = entityIndex;
        }

        public static EntityTemplate FromIndex(int entityIndex)
        {
            return new EntityTemplate(entityIndex);
        }

        public EntitySchema Entity => Context.Entities[_entityIndex];

        public override string NameSpace => $"{Context.NameSpaceRoot}.Model";

        public override string FileName => Entity.Name + ".cs";

        public override string FileContent => $@"{Profile}
using System;
using System.ComponentModel;

namespace {NameSpace}
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    public partial class {Entity.Name}
    {{
        {string.Join("\r\n        ", Entity.Properties.Select(x => $@"public {x.TypeName} {x.Name} {{ get; set; }}"))}
    }}
}}";
    }
}
