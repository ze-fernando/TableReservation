using API.Infra.Config;
using API.Models;
using System.Text.Json;
using StackExchange.Redis;

namespace API.Services;

public class RedisService(RedisConfig redisConfig, ReservationService service)
{
    private readonly IDatabase _database = redisConfig.GetDb();
    private readonly ReservationService _service = service;

    public async Task SetAsync<Reservation>(string key, Reservation reservation, TimeSpan expiration)
    {
        var jsonData = JsonSerializer.Serialize(reservation);
        await _database.StringSetAsync(key, jsonData, expiration);
    }


    public async Task<Reservation?> GetAsync(string key)
    {
        var jsonData = await _database.StringGetAsync(key);
        var result = jsonData.HasValue ? JsonSerializer.Deserialize<Reservation>(jsonData) : null;

        if (result != null)
            await _service.Create(result);

        return result;
    }
}
