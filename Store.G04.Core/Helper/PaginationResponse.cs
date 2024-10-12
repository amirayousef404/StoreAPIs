using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Helper
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pageSize, int pageIndex, int count, IEnumerable<TEntity> date)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Date = date;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int Count { get; set; }

        public IEnumerable<TEntity> Date { get; set; }
    }
}
