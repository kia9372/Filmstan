using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.CategoryCommands;
using DataTransfer.CategoryDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.CategoryQueries;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.CategoryControllers
{
    [DisplayName("مدیریت دسته بندی ها")]
    public class CategoryController : BaseController, IPermissionMarker
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [DisplayName("ایجاد دسته جدید")]
        public async Task<IActionResult> AddCategoryAsync(AddCategoryDto categoryDto)
        {
            var addCategory = await mediator.Send(new CreateCategoryCommand { Name = categoryDto.Name, ParentId = categoryDto.ParentId });
            if (addCategory.Success)
            {
                return Ok(addCategory.Result);
            }
            return BadRequest(addCategory.ErrorMessage);
        }


        [HttpPut]
        [DisplayName("ویرایش دسته ")]
        public async Task<IActionResult> UpdateCategory(EditCategoryDto categoryDto)
        {
            var updateCategory = await mediator.Send(new UpdateCategoryCommand { Id = categoryDto.Id, Name = categoryDto.Name, ParentId = categoryDto.ParentId });
            if (updateCategory.Success)
            {
                return Ok(updateCategory.Result);
            }
            return BadRequest(updateCategory.ErrorMessage);
        }

        [HttpDelete]
        [DisplayName("حذف دسته ")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var dleleteCategory = await mediator.Send(new DeleteCategoryCommand { Id = id });
            if (dleleteCategory.Success)
            {
                return Ok(dleleteCategory.Result);
            }
            return BadRequest(dleleteCategory.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryByIdAsync(Guid id)
        {
            var getCategory = await mediator.Send(new GetCategoryByIdQuery { Id = id });
            if (getCategory.Success)
            {
                return Ok(getCategory.Result);
            }
            return BadRequest(getCategory.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("نمایش لیست دسته ها ")]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var getCategory = await mediator.Send(new GetAllCategoryQuery());
            if (getCategory.Success)
            {
                return Ok(getCategory.Result);
            }
            return BadRequest(getCategory.ErrorMessage);
        }

    }
}