using System;
using System.Data;
using Dapper;

namespace p8mobility.persistence.Dapper;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        return new Guid((string) value);
    }
}