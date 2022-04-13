namespace ADORM.Domain.Objects.Template
{
    public class BaseEntityTemplate : BaseTemplate
    {
        public const string ClassName = "BaseEntity";
        
        public override string NameSpace => $"{Context.NameSpaceRoot}.Model";
        
        public override string FileName => ClassName + ".cs";
        
        public override string FileContent => $@"{Profile}
using System;

namespace {NameSpace}
{{
    public class {ClassName}
    {{
    }}
}}";
    }
}
