﻿namespace PasteBinClone.Web.Models
{
    public class ResponseAPI
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
