using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class CredentialsModel
    {
        //public string algorithm { get; set; }
        //public IDictionary<string, string> config { get; set; }
        //public int? counter { get; set; }
        //public long? createdDate { get; set; }
        //[JsonProperty("device")]
        //public string Device { get; set; }
        //[JsonProperty("digits")]
        //public int? Digits { get; set; }
        //[JsonProperty("hashIterations")]
        //public int? HashIterations { get; set; }
        //[JsonProperty("hashSaltedValue")]
        //public string HashSaltedValue { get; set; }
        //[JsonProperty("period")]
        //public int? Period { get; set; }
        //[JsonProperty("salt")]
        //public string Salt { get; set; }
        //[JsonProperty("temporary")]
        public bool? temporary { get; set; }
        //[JsonProperty("type")]
        public string? type { get; set; }
        //[JsonProperty("value")]
        public string value { get; set; }
    }
}
