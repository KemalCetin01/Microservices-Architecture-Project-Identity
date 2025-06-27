using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models

{
    public class jwtRolesObj
    {
        public List<string> roles { get; set; }
    }

    public class jwtRolesObjWithProp
    {
        public List<jwtRolesObj> obj { get; set; }
    }


}
