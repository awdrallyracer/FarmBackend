using Farm.Models;
using Farm.Models.DbModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Farm.Helpers
{
    public class InjectionTimerService : IHostedService, IDisposable
    {
        private Timer timer;

        public ApplicationContext appCtx;

        private IServiceScopeFactory serviceScopeFactory;
        public InjectionTimerService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void CancellInjection(Object? state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                appCtx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                List<Injection> injections = appCtx.Injections.ToList();
                List<Animal> animals = appCtx.Animals.ToList();

                var days = TimeSpan.FromMinutes(1);

                for (int i = 0; i < animals.Count; i++)
                {
                    foreach(Injection e in injections)
                    {     
                    if (e.AnimalId == animals[i].Id && (DateTime.Now - e.InjectionTime >= days) && animals[i].Injection == true)
                    {
                        animals[i].Injection = false;
                    }
                    appCtx.SaveChanges();
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(CancellInjection, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
