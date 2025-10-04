using Catalog.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Event_Handlers
{
    public class ProductPriceChnagedEventHandler(Logger<ProductPriceChnagedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
           logger.LogInformation("Domain event triggered", notification.GetType().Name);    
            return Task.CompletedTask;
        }
    }
}
