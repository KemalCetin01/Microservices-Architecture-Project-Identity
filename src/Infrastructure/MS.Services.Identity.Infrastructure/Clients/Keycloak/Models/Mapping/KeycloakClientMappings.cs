using MS.Services.Core.Networking.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class KeycloakClientMappings : IRestResponse
    {
        public string id { get; set; }
        public string client { get; set; }
        public List<RoleRepresentation> mappings { get; set; }
    }
}
