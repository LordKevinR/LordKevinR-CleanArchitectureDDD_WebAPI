namespace CleanArchitecture.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        // Task<List<User>> GetAllAsync();
        void Add(User user);
    }
}