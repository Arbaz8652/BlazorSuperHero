namespace BlazorSuperHero.Client.Services.SuperHeroServices
{
    public interface ISuperHeroService
    {
        List<SuperHero> Heroes { get; set; }
        List<Comic> Comics { get; set; }
        Task GetComic();
        Task GetSuperHero();
        Task<SuperHero>GetSingleHero(int id);

        Task CreatHero(SuperHero hero);

        Task UpdateHero(SuperHero hero);
        Task DeleteHero(int id);

    }
}
