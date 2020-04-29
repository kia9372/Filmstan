using Microsoft.AspNetCore.Http;
using System;

namespace DataTransfer.PostMagazine
{
    public class EditNewPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public string PostContent { get; set; }
        public string DownloadLink { get; set; }
        public string SubTitleLink { get; set; }
        public Guid CategoryId { get; set; }
    }
}
