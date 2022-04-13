using ADORM.Domain.Objects.Schema;
using ADORM.Domain.Roles.Template;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ADORM.Domain.Contexts
{
    public class TemplateContext
    {
        public const string OutputFolderName = "Output";

        public string NameSpaceRoot { get; }
        public string OutputFolder { get; }
        public List<ITemplate> Templates { get; }
        public EntitySchema[] Entities { get; set; }

        public TemplateContext(string nameSpaceRoot)
        {
            NameSpaceRoot = nameSpaceRoot;
            OutputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, OutputFolderName);
            if (!Directory.Exists(OutputFolder))
            {
                Directory.CreateDirectory(OutputFolder);
            }
            Templates = new List<ITemplate>();
        }

        public IEnumerable<Action> LoadEntities(TableSchema[] tables)
        {
            Entities = new EntitySchema[tables.Length];
            foreach (var index in Enumerable.Range(0, tables.Length))
            {
                yield return delegate
                {
                    Logger.Info($"Schema [{tables[index].Name}] is initializing...");
                    Entities[index] = new EntitySchema(tables[index]);
                };
            }
        }

        public IEnumerable<Action> Generate()
        {
            foreach (var template in Templates)
            {
                yield return delegate
                {
                    template.Generate();
                };
            }
        }

        public T Add<T>(T template) where T : ITemplate
        {
            template.Context = this;
            Templates.Add(template);
            if (template is IProjectTemplate)
            {
                if (!Directory.Exists(template.FolderPath))
                {
                    Directory.CreateDirectory(template.FolderPath);
                }
            }
            return template;
        }

        public void AddRange<T>(IEnumerable<T> templates) where T : ITemplate
        {
            foreach (var template in templates)
            {
                Add(template);
            }
        }

        public void AddByIndexes<T>(Func<int, T> builder) where T : ITemplate
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                Add(builder(i));
            }
        }
    }
}
