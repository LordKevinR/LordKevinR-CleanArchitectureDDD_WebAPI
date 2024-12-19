using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new("User.NotFound", "The user with that id was not found");
        public static Error InvalidCredentials = new("User.InvalidCredentials", "the credentials are invalid");
    }
}