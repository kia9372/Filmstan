using System;

namespace DataTransfer.CategoryDtos
{
    public class EditCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
