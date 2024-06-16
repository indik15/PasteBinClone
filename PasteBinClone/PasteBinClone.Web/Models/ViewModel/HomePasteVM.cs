﻿namespace PasteBinClone.Web.Models.ViewModel
{
    public class HomePasteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public CategoryVM Category { get; set; }
        public ContentTypeVM ContentType { get; set; }
        public LanguageVM Language { get; set; }
    }
}
