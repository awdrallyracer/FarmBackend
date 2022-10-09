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
    public class AnimalStateService : IHostedService, IDisposable
    {
        private Timer timer;

        public ApplicationContext appCtx;

        private IServiceScopeFactory serviceScopeFactory;
        public AnimalStateService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void ChangeState(Object? state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                appCtx = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                List<Food> feed = appCtx.Foods.ToList();
                List<Animal> animals = appCtx.Animals.ToList();

                var days = TimeSpan.FromMinutes(1);

                for (int i = 0; i < animals.Count; i++)
                {
                    foreach (Food e in feed)
                    {
                        if (e.AnimalId == animals[i].Id && e.Quantity != 0)
                        {
                            animals[i].Weight += 5; 
                            animals[i].Age += 1; 
                            e.Quantity --; 
                        } 


                        appCtx.SaveChanges();
                    }
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(ChangeState, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
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
