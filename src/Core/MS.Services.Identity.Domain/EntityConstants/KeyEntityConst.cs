using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.EntityConstants;

public class KeyEntityConst
{
    public const int EntityMaxLength = 511;

    public static class Identity
    {
        public static class Property
        {
            public const int PrefixMaxLength = 15;
        }
    }
}