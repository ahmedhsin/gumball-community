﻿namespace SocialMediaApp.ViewModels
{
    public class CreateCommentViewModel
    {
        public int PostId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }

    }
}
