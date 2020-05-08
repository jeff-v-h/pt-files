using System;

namespace PTFiles.Application.Common.Interfaces.Time
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
