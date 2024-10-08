using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G04.Core.Entities;
using Store.G04.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository
{
    public  class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            query.Include(spec.Includes[0]);

            query =  spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;

        }
    }
}
