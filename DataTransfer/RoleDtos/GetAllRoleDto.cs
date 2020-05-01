using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.RoleDtos
{
    public class GetAllFormQuery
    {
        public string Sorts { get; set; }
        public string Filters { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
