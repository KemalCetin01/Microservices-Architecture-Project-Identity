
namespace MS.Services.Identity.Domain.Entities;

public class AddressLocation : BaseSoftDeleteEntity
{
    /// <summary>
    ///     Country which the address is in.
    /// </summary>
    public string CountryCode { get; set; } = null!;
    /// <summary>
    ///     (Optional) City which the address is in. (şehir)
    /// </summary>
    public int? CityId { get; set; } 
    /// <summary>
    ///     (Optional) City which the address is in. Used if the user can not find city in the list.
    /// </summary>
    public string? CityName { get; set; }
    /// <summary>
    ///     (Optional) Town which the address is in. (ilçe)
    /// </summary>
    public int? TownId { get; set; }
    /// <summary>
    ///     (Optional) Town which the address is in. (ilçe) Used if the user can not find town in the list.
    /// </summary>
    public string? TownName { get; set; }
    /// <summary>
    ///     (Optional) District which the address is in. (Mahalle)
    /// </summary>
    public int? DistrictId { get; set; }
    /// <summary>
    ///     (Optional) District which the address is in. (Mahalle) Used if the user can not find district in the list.
    /// </summary>
    public string? DistrictName { get; set; }
    /// <summary>
    ///     (Optional) Zip or postal code
    /// </summary>
    public string? ZipCode { get; set; }
    /// <summary>
    ///     (Optional) Address details 1
    /// </summary>
    public string? AddressLine1 { get; set; }
    /// <summary>
    ///     (Optional) Address details 2
    /// </summary>
    public string? AddressLine2 { get; set; }
    /// <summary>
    ///     (Optional) Address Description
    /// </summary>
    public string? AddressDescription { get; set; }
    
}
