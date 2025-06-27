using Microsoft.EntityFrameworkCore;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.DataSeeds;

public class BusinessStatusData
{
    public static void SeedBusinessStatusData(ModelBuilder builder)
    {
        builder.Entity<BusinessStatus>().HasData(Enum.GetValues(typeof(BusinessStatusEnum))
            .Cast<BusinessStatusEnum>()
            .Select(e => new BusinessStatus
            {
                Id = (int) e,
                Name = e.ToString()
            }));
    }
}