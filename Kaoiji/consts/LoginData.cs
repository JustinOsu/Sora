﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaoiji.consts
{
    public class LoginData
    {
        public string Username;
        public string Password;
        public string OsuVersion;
        public int TimeOff;
        public SecurityHash SecurityHash;
        public bool BlockNonFriendsDM;
    }
    public class SecurityHash
    {
        public string OsuHash;
        public string DiskMD5;
        public string UniqueMD5;
    }
}