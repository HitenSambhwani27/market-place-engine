using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DDD
{
    public interface IDomainEvent: INotification
    {
        public DateTime OccurredOn => DateTime.UtcNow;
        public Guid Id => Guid.NewGuid();
        public string EventType => GetType().Name;
    }
}
