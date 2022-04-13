using ADORM.Domain.Objects.Schema;
using ADORM.Domain.Roles.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADORM.Domain.Objects.Template
{
    public class ReposProjectTemplate : BaseTemplate, IProjectTemplate
    {
        private readonly IProjectTemplate _entityProjectTemplate;

        public Guid ID { get; }

        public IEnumerable<EntitySchema> Entities { get; }

        public ReposProjectTemplate(IProjectTemplate entityProjectTemplate = null)
        {
            _entityProjectTemplate = entityProjectTemplate;
            ID = Guid.NewGuid();
        }

        public IEnumerable<EntitySchema> ValidEntities => Context.Entities.Where(x => x.Properties.Any(y => y.IsIdentity || y.IsPrimaryKey));

        public override string NameSpace => $"{Context.NameSpaceRoot}.{ReposTemplate.FolderName}";

        public override string FileName => $"{NameSpace}.csproj";

        public override string FileContent => $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""15.0"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectGuid>{{{ID.ToString().ToUpper()}}}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>{NameSpace}</RootNamespace>
    <AssemblyName>{NameSpace}</AssemblyName>
    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""System"" />
    <Reference Include=""System.Configuration"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Transactions"" />
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Net.Http"" />
    <Reference Include=""System.Xml"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""{DatabaseTemplate.ClassName}.cs"" />
    <Compile Include=""{BaseReposTemplate.ClassName}.cs"" />
    {string.Join("\r\n    ", ValidEntities.Select(x => $"<Compile Include=\"{x.PascalName}{ReposTemplate.Suffix}.cs\" />"))}
  </ItemGroup>{(_entityProjectTemplate == null ? string.Empty : $@"
  <ItemGroup>
    <ProjectReference Include=""..\{_entityProjectTemplate.NameSpace}\{_entityProjectTemplate.FileName}"">
      <Project>{{{_entityProjectTemplate.ID.ToString().ToUpper()}}}</Project>
      <Name>{_entityProjectTemplate.NameSpace}</Name>
    </ProjectReference>
  </ItemGroup>")}
  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
</Project>";
    }
}
