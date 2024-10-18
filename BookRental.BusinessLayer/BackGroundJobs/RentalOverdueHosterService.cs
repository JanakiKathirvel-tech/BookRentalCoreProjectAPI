using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.BusinessLayer.BackGroundJobs
{
    public class RentalOverdueHosterService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EmailService _emailService;

        public RentalOverdueHosterService(IServiceProvider serviceProvider, EmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Job Started...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Job Stoped...");
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
