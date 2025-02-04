using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Rentals.GetRental
{
    internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetRentalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<RentalResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
                SELECT
                    id AS Id,
                    vehicle_id AS VehicleId,
                    user_id AS UserId,
                    status AS Status,
                    rental_price AS RentalPrice,
                    rental_currency_type AS RentalCurrencyType,
                    maintenance_price AS MaintenancePrice,
                    maintenance_currency_type AS MaintenanceCurrencyType,
                    accessories_price AS AccessoriesPrice,
                    accessories_currency_type AS AccessoriesCurrencyType,
                    total_price AS TotalPrice,
                    total_currency_type AS TotalCurrencyType,
                    start_date AS StartDate,
                    end_date AS EndDate,
                    created_at AS CreatedAt
                FROM rental WHERE id = @RentalId
            """;

            var rental = await connection.QueryFirstOrDefaultAsync<RentalResponse>(
                sql,
                new { request.RentalId });


            return rental!;
        }
    }
}