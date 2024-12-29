using StackExchange.Redis;

namespace API.Infra.Config;

public class RedisConfig(string connection)
{
    private readonly ConnectionMultiplexer _redis = ConnectionMultiplexer
        .Connect(connection);

    public IDatabase GetDb() => _redis.GetDatabase();
}
