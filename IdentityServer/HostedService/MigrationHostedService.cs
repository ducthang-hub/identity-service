using IdentityServer.Repository;
using IdentityServer.Services.DatabaseService;

namespace IdentityServer.HostedService
{
    public class MigrationHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDatabaseService _dbService;
        public MigrationHostedService(
                IServiceProvider serviceProvider,
                IDatabaseService dbService
            )
        {
            _serviceProvider = serviceProvider;            
            _dbService = dbService;
        }
        public void Dispose()
        {
            Console.WriteLine("Dispose Hosted Service");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            const string funcName = $"{nameof(MigrationHostedService)} - {nameof(StartAsync)}";
            try
            {
                Console.WriteLine($"{funcName} Start Migrate Database");

                await _dbService.MigrateDb();

                Console.WriteLine($"{funcName} Finish Migrate Dastabase");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{funcName} has error: {ex.Message}");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop Hosted Service");
        }
    }
}
