﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.ViewModels
{
    public class CommentsResponse
    {
        public IEnumerable<CommentVM> CommentVMs { get; set; }
        public int TotalPages { get; set; }
    }
}
