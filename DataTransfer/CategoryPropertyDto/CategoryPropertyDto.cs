using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.CategoryPropertyDto
{
    public class CategoryPropertyDto
    {
        public Guid? Id { get; set; }
        public Guid CategoryId { get; set; }
        public string PropName { get; set; }
        public CategoryPropertyType CategoryPropertyType { get; set; }
    }
}
