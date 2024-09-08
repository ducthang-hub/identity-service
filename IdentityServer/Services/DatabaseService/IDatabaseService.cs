namespace IdentityServer.Services.DatabaseService
{
    public interface IDatabaseService
    {
        public Task<bool> MigrateDb(int attempt = 1);
    }
}
