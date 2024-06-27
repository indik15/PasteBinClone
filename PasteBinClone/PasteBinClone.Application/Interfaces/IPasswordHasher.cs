﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string PasswordHash(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
