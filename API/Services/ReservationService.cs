using API.Dtos;
using API.Models;
using API.Infra.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class ReservationService
{
    private readonly IMongoCollection<Reservation> _collection;

    public ReservationService(
          IOptions<MongoConfig> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Reservation>(
           DatabaseSettings.Value.Collection);

    }


    public async Task<List<Reservation>> FindAll()
    {
        var reservations = await _collection.Find(_ => true).ToListAsync();
        return reservations;
    }

    public async Task<Reservation?> FindById(string id)
    {
        var reservation = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return reservation;

    }

    public async Task Create(Reservation resv)
    {
        resv.Id = "";
        await _collection.InsertOneAsync(resv);
    }

    public async Task<Reservation> Update(string id, ReservationDto resv)
    {
        var reservation = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        reservation.NumberPeople = resv.NumberOfPeople;
        reservation.Date = resv.Date;
        reservation.ReservationName = resv.ReservationName;
        reservation.EmailOfClient = resv.Email;

        await _collection.ReplaceOneAsync(x => x.Id == id, reservation);
        return reservation;
    }

    public async Task Delete(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

}
