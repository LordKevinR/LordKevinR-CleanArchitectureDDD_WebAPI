using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using Dapper;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    internal sealed class SearchVehicleQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
    {
        private static readonly int[] ActiveRentalStatuses = {
            (int)RentalStatus.Reserved,
            (int)RentalStatus.Confirmed,
            (int)RentalStatus.Completed
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SearchVehicleQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
        {
            if (request.endDate > request.startDate)
            {
                return new List<VehicleResponse>();
            }

            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = @"
                SELECT
                    id AS Id,
                    model AS Model,
                    vin AS Vin,
                    price AS Price,
                    currency_type AS CurrencyType,
                    address_country AS Country,
                    address_department AS Department,
                    address_province AS Province,
                    address_street AS Street
                FROM vehicles
                WHERE id NOT IN (
                    SELECT vehicle_id
                    FROM rental
                    WHERE start_date <= @EndDate
                    AND end_date >= @StartDate
                    AND status = ANY(@ActiveRentalStatuses)
                )";

            var vehicles = await connection.QueryAsync<VehicleResponse, AddressResponse, VehicleResponse>(
                sql,
                (vehicles, address) =>
                {
                    vehicles.Address = address;
                    return vehicles;
                },
                new
                {
                    StardDate = request.startDate,
                    EndDate = request.endDate,
                    ActiveRentalStatuses
                },
                splitOn: "Country"
                );

            return vehicles.ToList();
        }
    }
}