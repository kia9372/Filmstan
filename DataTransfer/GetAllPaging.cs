using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer
{
    public class GetAllPaging<TEntity> where TEntity : class
    {
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Records { get; set; }
    }
}
