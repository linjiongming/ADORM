using System;
using System.IO;

namespace ADORM.Domain.Objects.Template
{
    public class EntityAssemblyInfoTemplate : BaseTemplate
    {
        private readonly EntityProjectTemplate _entityProjectTemplate;

        public EntityAssemblyInfoTemplate(EntityProjectTemplate entityProjectTemplate)
        {
            _entityProjectTemplate = entityProjectTemplate;
        }

        public override string NameSpace => $"{Context.NameSpaceRoot}.Model";

        public override string FolderPath => Path.Combine(Context.OutputFolder, NameSpace, "Properties");

        public override string FileName => "AssemblyInfo.cs";

        public override string FileContent => $@"
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
[assembly: AssemblyTitle(""{NameSpace}"")]
[assembly: AssemblyDescription("""")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyCompany("""")]
[assembly: AssemblyProduct(""{NameSpace}"")]
[assembly: AssemblyCopyright(""Copyright ©  {DateTime.Today.Year}"")]
[assembly: AssemblyTrademark("""")]
[assembly: AssemblyCulture("""")]
[assembly: ComVisible(false)]
[assembly: Guid(""{_entityProjectTemplate.ID.ToString().ToLower()}"")]
[assembly: AssemblyVersion(""1.0.0.0"")]
[assembly: AssemblyFileVersion(""1.0.0.0"")]";

        public override void Generate()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            base.Generate();
        }
    }
}
