using Dtos;
using Models;
using Infra.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Services;

public class ReservationServices
{
    private readonly IMongoCollection<Reservation> _collection;

    public ReservationServices(
          IOptions<MongoConfig> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<Reservation>(
           DatabaseSettings.Value.Collection);
    }

    public async Task<List<Reservation>> FindAll() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Reservation?> FindById(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Reservation> Create(ReservationDto resv)
    {
        Reservation newReservation = new Reservation
        {
            NumberPeople = resv.NumberOfPeople,
            Date = resv.Date,
            ReservationName = resv.ReservationName,
            EmailOfClient = resv.Email,
            IsConfirmed = false
        };

        await _collection.InsertOneAsync(newReservation);
        return newReservation;
    }

    public async Task<Reservation> Update(string id, ReservationDto resv)
    {
        var reservation = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        reservation.NumberPeople = resv.NumberOfPeople;
        reservation.Date = resv.Date;
        reservation.ReservationName = resv.ReservationName;
        reservation.EmailOfClient = resv.Email;
        reservation.IsConfirmed = false;

        await _collection.ReplaceOneAsync(x => x.Id == id, reservation);
        return reservation;
    }

    public async Task Delete(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);

}
