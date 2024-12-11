namespace Domain.Exceptions
{
    public sealed class PlayerNotFoundException(int id) : Exception($"Player with id={id} not found!");
}
