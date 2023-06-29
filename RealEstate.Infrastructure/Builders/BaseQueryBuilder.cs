namespace RealEstate.Infrastructure.Builders
{
    public abstract class BaseQueryBuilder<TEntity>
    {
        public IQueryable<TEntity> Query { get; set; }

        public BaseQueryBuilder(IQueryable<TEntity> query)
        {
            Query = query;
        }

        public BaseQueryBuilder<TEntity> ApplyPagination(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var startIndex = (pageNumber - 1) * pageSize;
            Query = Query.Skip(startIndex).Take(pageSize);
            return this;
        }

        public IQueryable<TEntity> GetQuery()
        {
            return Query;
        }
    }
}
