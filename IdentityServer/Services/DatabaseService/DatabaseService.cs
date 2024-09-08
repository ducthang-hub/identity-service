using IdentityServer.Constants;
using IdentityServer.Repository;

namespace IdentityServer.Services.DatabaseService
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IServiceProvider _serviceProvider;
        public DatabaseService(
                IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<bool> MigrateDb(int attempt = 1)
        {
            const string funcName = $"{nameof(MigrateDb)}";
            try
            {
                if(attempt > GeneralConstants.DbMigrateAttempt)
                {
                    return false;
                }

                using var scope = _serviceProvider.CreateScope();
                var uor = scope.ServiceProvider.GetRequiredService<IUnitOfRepository>();
                uor.Migrate();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{funcName} {ex.Message}");
                Thread.Sleep(10000);
                var retryResult = await MigrateDb(++attempt);
                return retryResult;
            }
        }
    }
}
