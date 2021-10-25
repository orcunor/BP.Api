using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BP.Api.BackgroundServices
{
    public class DateTimeLogWriter : IHostedService , IDisposable
    {
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DateTimeLogWriter)} Service Started...");

            timer = new Timer(WriteDateTimeOnLog,null,TimeSpan.Zero,TimeSpan.FromSeconds(1));

            //while (!cancellationToken.IsCancellationRequested) // cancel etmediği sürece yapıcaz bu işi
            //{
            //    WriteDateTimeOnLog();
            //    await Task.Delay(1000);
            //}

            return Task.CompletedTask;
        }

        private void WriteDateTimeOnLog(object state)
        {
            Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DateTimeLogWriter)} Service Stopped...");

            timer?.Change(Timeout.Infinite, 0); // timer durucak
            DisposeTimer();
            return Task.CompletedTask;
        }

        public void Dispose()
        {

            DisposeTimer();
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
    }
}
