namespace MercurySchool.Api.Repositories;

public class RepositoryBase
{
    internal static string CreateSqlConnectionString(DatabaseOptions options)
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = options.DataSource,
            InitialCatalog = options.InitialCatalog,
            UserID = options.UserID,
            Password = options.Password,
            Encrypt = options.Encrypt,
            ConnectTimeout = options.ConnectTimeout,
            MultipleActiveResultSets = options.MultipleActiveResultSets,
            TrustServerCertificate = options.TrustServerCertificate,
            PersistSecurityInfo = options.PersistSecurityInfo
        };

        return connectionStringBuilder.ConnectionString;
    }

    internal static string CreateCommandText(string methodName)
    {
        var suffixStart = methodName.LastIndexOf("Async");
        var storeProcedureName = methodName[..suffixStart];

        return $"api.{storeProcedureName}";
    }
}
