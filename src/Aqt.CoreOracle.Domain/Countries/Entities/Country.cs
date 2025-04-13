using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Aqt.CoreOracle.Domain.Shared.Countries;

namespace Aqt.CoreOracle.Domain.Countries.Entities;

public class Country : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; private set; }
    public string Name { get; set; }

    protected Country() { /* For ORM */ }

    public Country(
        Guid id,
        [NotNull] string code,
        [NotNull] string name)
        : base(id)
    {
        SetCode(code);
        SetName(name);
    }

    internal void SetCode([NotNull] string code)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), CountryConsts.MaxCodeLength);
        Code = code;
    }

    internal void SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), CountryConsts.MaxNameLength);
        Name = name;
    }
} 