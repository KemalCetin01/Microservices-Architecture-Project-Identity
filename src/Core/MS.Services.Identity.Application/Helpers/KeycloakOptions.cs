namespace MS.Services.Identity.Application.Helpers;

public class KeycloakOptions
{
    public string master_realm { get; set; } = null!;
    public string ozdisan_realm { get; set; } = null!;
    public string ecommerce_b2b_realm { get; set; } = null!;
    public string ecommerce_b2c_realm { get; set; } = null!;
    public string grant_type { get; set; } = null!;
    public string ecommerce_grant_type { get; set; } = null!;
    public string msidentity_client_id { get; set; } = null!;
    public string ecommerce_b2c_client_id { get; set; } = null!;
    public string ecommerce_b2c_client_ref_id { get; set; } = null!;
    public string ecommerce_b2b_client_id { get; set; } = null!;
    public string ecommerce_b2b_client_ref_id { get; set; } = null!;
    public string msidentity_client_secret { get; set; } = null!;
    public string ecommerce_b2c_client_secret { get; set; } = null!;
    public string ecommerce_b2b_client_secret { get; set; } = null!;
    public string ecommerce_scope { get; set; } = null!;
    public string base_address { get; set; } = null!;
    public string refresh_token_grant_type { get; set; } = null!;
    public int user_update_retry_count { get; set; }
}
