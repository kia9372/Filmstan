using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Command.BrandCommands;
using Common;
using DataTransfer.BrandDtos;
using DataTransfer.RoleDtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.BrandQueries;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.BrandControllers
{
    [DisplayName("مدیریت برندها")]
    public class BrandController : BaseController,IPermissionMarker
    {
        private readonly IMediator mediator;

        public BrandController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [DisplayName("ایجاد برند جدید")]
        public async Task<IActionResult> AddBrand(AddBrandDto addBrand)
        {
            var add = await mediator.Send(new CreateBrandCommand {ISOBrandName=addBrand.ISOBrandName, BrandName = addBrand.BrandName, CategoryId = addBrand.CategoryId });
            if (add.Success)
            {
                return Ok();
            }
            return BadRequest(add.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("ویرایش برند ")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var updateBrand = await mediator.Send(new UpdateBrandCommand
            {
                BrandId = updateBrandDto.BrandId,
                ISOBrandName= updateBrandDto.ISOBrandName,
                CategoryId = updateBrandDto.CategoryId,
                BrandName = updateBrandDto.BrandName
            });
            if (updateBrand.Success)
            {
                return Ok();
            }
            return BadRequest(updateBrand.ErrorMessage);
        }

        [HttpDelete]
        [DisplayName("حذف برند ")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var updateBrand = await mediator.Send(new DeleteBrandCommand
            {
                BrandId = id
            });
            if (updateBrand.Success)
            {
                return Ok();
            }
            return BadRequest(updateBrand.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست برندها  ")]
        public async Task<IActionResult> GetAllBrandByCategory(Guid categoryId)
        {
            var getBrand = await mediator.Send(new GetBrandByCategoryId
            {
                categoryId = categoryId
            });
            if (getBrand.Success)
            {
                return Ok(getBrand.Result);
            }
            return BadRequest(getBrand.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست برندها به صورت صفحه بندی")]
        public async Task<IActionResult> GetAllBrandByPaging([FromQuery]GetAllFormQuery getAllbrands)
        {
            var getBrand = await mediator.Send(new GetAllBrandPaging
            {
                Filters = getAllbrands.Filters,
                Page = getAllbrands.Page,
                PageSize = getAllbrands.PageSize,
                Sorts = getAllbrands.Sorts
            });
            if (getBrand.Success)
            {
                return Ok(getBrand.Result);
            }
            return BadRequest(getBrand.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("نمایش برند به صورت تکی")]
        [Route("{id}")]
        public async Task<IActionResult> GetBrandById(Guid id)
        {
            var getBrand = await mediator.Send(new GetBrandById
            {
                brandId = id
            });
            if (getBrand.Success)
            {
                return Ok(getBrand.Result);
            }
            return BadRequest(getBrand.ErrorMessage);
        }
    }
}