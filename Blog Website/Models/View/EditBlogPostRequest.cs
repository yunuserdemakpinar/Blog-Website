﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Website.Models.View
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public IEnumerable<SelectListItem> AvailableTags { get; set; }
        public string[] SelectedTagIds { get; set; } = Array.Empty<string>();
    }
}
