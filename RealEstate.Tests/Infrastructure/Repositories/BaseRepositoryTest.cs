using Microsoft.EntityFrameworkCore;

namespace RealEstate.Tests.Infrastructure.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="BaseRepositoryTest"/> class.
    /// </summary>
    [SetUpFixture]
    public class BaseRepositoryTest
    {
        protected DbContextOptions<RealEstateDBContext> _dbContextOptions;

        /// <summary>
        /// Initial setup that runs before each all tests.
        /// </summary>
        [OneTimeSetUp]
        public void SetUpBase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RealEstateDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
    }
}
