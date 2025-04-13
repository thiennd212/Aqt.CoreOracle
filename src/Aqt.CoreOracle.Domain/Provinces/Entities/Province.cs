using System;
using Aqt.CoreOracle.Domain.Countries.Entities;
using Aqt.CoreOracle.Domain.Shared.Provinces;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Aqt.CoreOracle.Domain.Provinces.Entities;

public class Province : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid CountryId { get; private set; }
    public virtual Country Country { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }

    protected Province() { /* For ORM */ }

    public Province(Guid id, Guid countryId, [NotNull] string code, [NotNull] string name) : base(id)
    {
        SetCountry(countryId);
        SetCode(code);
        SetName(name);
    }

    public void SetCountry(Guid countryId)
    {
        CountryId = countryId;
    }

    public void SetCode([NotNull] string code)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), ProvinceConsts.MaxCodeLength);
        Code = code;
    }

    public void SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ProvinceConsts.MaxNameLength);
        Name = name;
    }

    internal void ChangeCountry(Guid newCountryId)
    {
        SetCountry(newCountryId);
    }
} 