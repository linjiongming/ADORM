using ADORM.Domain.Contexts;

namespace ADORM.Domain.Roles.Template
{
    public interface ITemplate
    {
        TemplateContext Context { get; set; }
        string NameSpace { get; }
        string FolderPath { get; }
        string FileName { get; }
        string FilePath { get; }
        string FileContent { get; }
        void Generate();
    }
}
