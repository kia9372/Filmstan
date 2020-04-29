using Localization.Resources.Translations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataTransfer.PostMagazine
{
    public class AddNewPostDto
    {
        [Display(Name =ValidationTranslate.Error)]
        [StringLength(1, ErrorMessage = ValidationTranslate.Error)]
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public string PostContent { get; set; }
        public string DownloadLink { get; set; }
        public string SubTitleLink { get; set; }
        public Guid CategoryId { get; set; }
    }
}
