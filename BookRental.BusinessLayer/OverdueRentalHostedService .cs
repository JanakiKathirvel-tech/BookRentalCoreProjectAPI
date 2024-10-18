using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer
{
    public class OverdueRentalHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly EmailService _emailService;

        public OverdueRentalHostedService(IServiceProvider serviceProvider, EmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Set the task to run once a day
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                Console.WriteLine("job started...");
                //using (var scope = _serviceProvider.CreateScope())
                //{
                //    var rentalService = scope.ServiceProvider.GetRequiredService<RentalService>();
                //    await rentalService.MarkOverdueRentalsAsync();

                //    var overdueRentals = await rentalService.GetOverdueRentalsAsync();

                //    foreach (var rental in overdueRentals)
                //    {
                //        // Assuming rental has UserEmail property
                //        var emailBody = $"Dear Customer, your rental for {rental.Book.Title} is overdue.";
                //        //  await _emailService.SendEmailAsync(rental.UserId, "Overdue Rental Notification", emailBody);
                //        await _emailService.SendEmailAsync("janaki.kv2@gmail.com", "Overdue Rental Notification", emailBody);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Overduerentalservice error");
                Console.WriteLine(ex.ToString());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
