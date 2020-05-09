using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.BrandDtos
{
    public class AddBrandDto
    {
        public string BrandName { get; set; }
        public string ISOBrandName { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class UpdateBrandDto
    {
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string ISOBrandName { get; set; }
        public Guid CategoryId { get; set; }
    }
}
