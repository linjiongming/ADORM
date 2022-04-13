namespace ADORM.Domain.Objects.Template
{
    public class BaseReposTemplate : BaseTemplate
    {
        public const string ClassName = "BaseProvider";

        public override string NameSpace => $"{Context.NameSpaceRoot}.DAL";

        public override string FileName => $"{ClassName}.cs";

        public override string FileContent => $@"{Profile}
using System;

namespace {NameSpace}
{{
    public class {ClassName}
    {{
        protected Database DB {{ get; set; }}

        public {ClassName}()
        {{
            DB = Database.GetDefault();
        }}
    }}
}}";
    }
}
