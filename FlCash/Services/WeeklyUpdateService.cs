using System;
using System.Threading;
using System.Threading.Tasks;
using FlCash.Data;
using FlCash.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace FlCash.Services
{
    public class WeeklyPointsUpdateBackgroundService : BackgroundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public WeeklyPointsUpdateBackgroundService(IUnitOfWork unitOfWork, IServiceScopeFactory serviceScopeFactory)
        {
            _unitOfWork = unitOfWork;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime currentDate = DateTime.UtcNow;
                if (currentDate.DayOfWeek == DayOfWeek.Friday) // Start of the week is Friday
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                        var users = await context.Users.Where(x => x.PointsInWeek > 0).ToListAsync();
                        foreach (var user in users)
                        {
                            user.PointsInWeek = 0; // Reset weekly points to 0
                        }
                        await context.SaveChangesAsync(); // Save changes to the database
                    }
                }

                if (DateTime.Now.Day == 1)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                        var users = await context.Users.Where(x => x.PointsInMonth > 0).ToListAsync();
                        foreach (var user in users)
                        {
                            user.PointsInMonth = 0; // Reset monthly points to 0
                        }
                        await context.SaveChangesAsync(); // Save changes to the database
                    }
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Wait for 1 minute before checking again
            }
        }
    }
}
