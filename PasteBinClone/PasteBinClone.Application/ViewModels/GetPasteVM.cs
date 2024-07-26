﻿using PasteBinClone.Application.Dto;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.ViewModels
{
    public class GetPasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublic { get; set; }
        public ulong Likes { get; set; }
        public ulong Dislikes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM Type { get; set; }
        public LanguageVM Language { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<CommentVM> Comments { get; set; }

        //Properties to display if a user likes or dislikes a Paste.
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
    }
}
