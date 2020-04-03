using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentProcessor
{
    public class DocumentProcessor : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                Task.Delay(TimeSpan.FromSeconds(1));
            }

            return Task.CompletedTask;
        }
    }
}
