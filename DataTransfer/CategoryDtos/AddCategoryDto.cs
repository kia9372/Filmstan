using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.CategoryDtos
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
