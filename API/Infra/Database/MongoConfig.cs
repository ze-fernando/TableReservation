namespace Infra.Config;

public class MongoConfig
{
    public string ConnectionString { get; set; } = null!;

    public string Database { get; set; } = null!;

    public string CollectionUser { get; set; } = null!;

    public string CollectionReservation { get; set; } = null!;
}
