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
    public class BarnService : IHostedService, IDisposable
    {
        private Timer timer;

        public ApplicationContext appCtx;

        private IServiceScopeFactory serviceScopeFactory;
        public BarnService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void ChangeConditions(Object? state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                appCtx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                List<Barn> barns = appCtx.Barns.ToList();
                List<Animal> animals = appCtx.Animals.ToList();

                var days = TimeSpan.FromMinutes(1);

                for (int i = 0; i < animals.Count; i++)
                {
                    foreach (Barn b in barns)
                    {
                        if (animals[i].BarnId == b.Id)
                        {
                            b.Conditions = "Dirty"; 
                        } 
                        appCtx.SaveChanges();
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(ChangeConditions, null, TimeSpan.Zero, TimeSpan.FromMinutes(20));
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
