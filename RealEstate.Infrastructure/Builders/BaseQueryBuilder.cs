namespace RealEstate.Infrastructure.Builders
{
    /// <summary>
    /// Represents a base class for building queries on an entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public abstract class BaseQueryBuilder<TEntity>
    {
        /// <summary>
        /// Gets or sets the queryable object representing the entity.
        /// </summary>
        public IQueryable<TEntity> Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryBuilder{TEntity}"/> class with the specified query.
        /// </summary>
        /// <param name="query">The queryable object representing the entity.</param>
        public BaseQueryBuilder(IQueryable<TEntity> query)
        {
            Query = query;
        }

        /// <summary>
        /// Applies pagination to the query.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The updated query builder.</returns>
        public BaseQueryBuilder<TEntity> ApplyPagination(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var startIndex = (pageNumber - 1) * pageSize;
            Query = Query.Skip(startIndex).Take(pageSize);
            return this;
        }

        /// <summary>
        /// Gets the final query.
        /// </summary>
        /// <returns>The query representing the entity.</returns>
        public IQueryable<TEntity> GetQuery()
        {
            return Query;
        }
    }
}
