namespace Domain.Entities
{
    public sealed class Country(string picture, string code)
    {
        public string Picture { get; } = picture;
        public string Code { get; } = code;
    }
}
