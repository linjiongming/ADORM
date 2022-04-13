
using System.Data.Common;

namespace ADORM.Domain.Objects.Generator
{
    public class GeneratorOption
    {
        public string ConnectionString { get; set; }
        public DbProviderFactory DbProviderFactory { get; set; }
        public string NameSpace { get; set; }
        public bool GenerateEntities { get; set; } = true;
        public bool GenerateRepositories { get; set; } = true;
        public string Tables { get; set; }

        public GeneratorOption()
        {

        }

        public GeneratorOption(string[] args)
        {
            if (args.TryGetArg("ns", out var nameSpace))
            {
                NameSpace = nameSpace;
            }
            else
            {
                NameSpace = "My";
            }
            if (args.TryGetArg("conn", out var connstr))
            {
                ConnectionString = connstr;
            }
            if (args.TryGetArg("provider", out var provider))
            {
                DbProviderFactory = Const.Schema.GetFactory(provider);
            }
            else
            {
                DbProviderFactory = Const.Schema.GetFactory("sql");
            }
            if (args.TryGetArg("t", out var tables))
            {
                Tables = tables;
            }
            //GenerateEntities = args.TryGetArg("gene", out _);
            //GenerateRepositories = args.TryGetArg("genr", out _);
        }

    }
}
