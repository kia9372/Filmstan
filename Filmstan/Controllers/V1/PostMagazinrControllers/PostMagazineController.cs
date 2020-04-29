using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Command.PostMagazinrCommands;
using Common.HttpContextExtentions;
using DataTransfer.PostMagazine;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.PostMagazaineQueries;
using Travel.Framework.Base;

namespace Filmstan.Controllers.V1.PostMagazinrControllers
{
    [DisplayName("مدیریت پست ها")]
    public class PostMagazineController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContext;

        public PostMagazineController(IMediator mediator,IHttpContextAccessor httpContext) : base(mediator)
        {
            this.mediator = mediator;
            this.httpContext = httpContext;
        }

        [HttpPost]
        [DisplayName("ایجاد پست جدید")]
        public async Task<IActionResult> AddNewsPost([FromForm]AddNewPostDto newPostDto)
        {
            var result = await mediator.Send(new CreatePostMagazineCommands
            {
                CategoryId = newPostDto.CategoryId,
                Description = newPostDto.Description,
                DownloadLink = newPostDto.DownloadLink,
                Photo = newPostDto.Photo,
                PostContent = newPostDto.PostContent,
                SubTitleLink = newPostDto.SubTitleLink,
                Title = newPostDto.Title,
                WriterId =Guid.Parse(httpContext.HttpContext.User.Identity.GetUserId())
            });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        [DisplayName("ویرایش پست ")]
        public async Task<IActionResult> UpdatePost([FromForm]EditNewPostDto newPostDto)
        {
            var result = await mediator.Send(new UpdatePostMagazineCommands
            {
                Id = newPostDto.Id,
                CategoryId = newPostDto.CategoryId,
                Description = newPostDto.Description,
                DownloadLink = newPostDto.DownloadLink,
                Photo = newPostDto.Photo,
                PostContent = newPostDto.PostContent,
                SubTitleLink = newPostDto.SubTitleLink,
                Title = newPostDto.Title,
                WriterId = Guid.Parse(httpContext.HttpContext.User.Identity.GetUserId())
            });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete]
        [DisplayName("حذف پست ")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await mediator.Send(new DeletetPostMagazineCommands
            {
                id = id
            });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست پست ها ")]
        public async Task<IActionResult> GetAllPost()
        {
            var result = await mediator.Send(new GetAllPosQuery());
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست پست های یک نویسنده ")]
        public async Task<IActionResult> GetAllByWriterId()
        {
            var result = await mediator.Send(new GetAllPosQuery());
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        [DisplayName("لیست پست های یک دسته ")]
        public async Task<IActionResult> GetAllByCategoryId(Guid categoryId)
        {
            var result = await mediator.Send(new GetPostByCategoryIdQuery { categoryId = categoryId });
            if (result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorMessage);
        }

    }
}