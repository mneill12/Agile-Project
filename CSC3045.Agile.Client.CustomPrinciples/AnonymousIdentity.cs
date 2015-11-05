﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC3045.Agile.Client.Entities;


namespace CSC3045.Agile.Client.CustomPrinciples
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, new UserRole[] { })
        { }
    }

}
