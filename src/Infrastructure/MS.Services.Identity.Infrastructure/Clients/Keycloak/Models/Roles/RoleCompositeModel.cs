
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class RoleCompositeModel
    {
        public IDictionary<string, string> Client { get; set; }
        public IEnumerable<string> Realm { get; set; }
    }
}
