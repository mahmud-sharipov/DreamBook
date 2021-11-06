﻿using DreamBook.Application.Abstraction.Response;
using DreamBook.Application.LanguageResources;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamBook.Application.Posts
{
    public class PostResponseModel : IResponseModel
    {
        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Entity_Guid))]
        public Guid Guid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Title))]
        public string Title { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Content))]
        public string Content { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Image))]
        public string Image { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Category))]
        public Guid CategoryGuid { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_Category))]
        public string CategoryName { get; set; }

        [Display(ResourceType = typeof(ModelsLabel), Name = nameof(ModelsLabel.Post_DateTime))]
        public DateTime CreatedAt { get; set; }
    }
}
