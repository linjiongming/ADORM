using System;

namespace ADORM.Domain.Roles.Template
{
    public interface IProjectTemplate : ITemplate
    {
        Guid ID { get; }
    }
}
