using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Persistence.DataSeeds;

public class IdentityKeyData
{
    public static void SeedIdentityKeyDatas(ModelBuilder builder)
    {
   
        builder.Entity<UserType>().HasData(
            new UserType
            {
                Id = 1,
                Name="B2B"
            }
        );
    }
}
