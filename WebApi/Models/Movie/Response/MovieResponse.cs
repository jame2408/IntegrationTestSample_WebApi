namespace WebApi.Models.Movie.Response
{
    public sealed record MovieResponse
    {
        public MovieResponse(Database.Models.Movie s)
        {
            Name = s.Name;
        }

        public string Name { get; init; }
    }
}