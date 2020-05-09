using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.BrandDtos
{
    public class GetAllBrands
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string BrandName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string IsoBrandName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string CategoryName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public Guid CategoryId { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]
        public Guid Id { get; set; }
    }
}
