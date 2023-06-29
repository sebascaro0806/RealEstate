using Microsoft.EntityFrameworkCore;

namespace RealEstate.Tests.Infrastructure.Repositories
{
    [SetUpFixture]
    public class BaseRepositoryTest
    {
        protected DbContextOptions<RealEstateDBContext> _dbContextOptions;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RealEstateDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
    }
}
