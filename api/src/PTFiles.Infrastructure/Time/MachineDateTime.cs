using PTFiles.Application.Common.Interfaces.Time;
using System;

namespace PTFiles.Infrastructure.Time
{
    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
