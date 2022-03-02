using Microsoft.Extensions.Hosting;
using Serilog;

namespace Domain.Services
{
    public class RobotService : IHostedService, IDisposable

    {
        private int executionCount = 0;
        private Timer _timer = null!;
        private readonly RobotDomain _robotDomain;

        public RobotService(RobotDomain robotDomain)
        {
            _robotDomain = robotDomain;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Log.Information("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            Log.Information("Timed Hosted Service is working. Count: {Count}", count);

            _robotDomain.ExecuteRobotJob();

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Information("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}