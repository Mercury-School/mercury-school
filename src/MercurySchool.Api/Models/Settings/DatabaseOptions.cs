namespace MercurySchool.Api.Models.Settings;

public class DatabaseOptions
{
    public string DataSource { get; init; }
    public string InitialCatalog { get; init; }
    public string Password { get; init; }
    public string UserID { get; init; }
    public bool PersistSecurityInfo => false;
    public int ConnectTimeout => 120;
    public bool MultipleActiveResultSets => false;
    public bool Encrypt => true;
    public bool TrustServerCertificate => false;
}
