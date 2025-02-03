using System;
using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
    private readonly List<Genre> genres =
    [
        new Genre { Id = new Guid("524c6596-dc0c-4de6-ab5e-eebcbeefa162"), Name = "Fighting"},
        new Genre { Id = new Guid("17cf2925-6965-4716-b21b-26a0968596b0"), Name = "Roleplaying"},
        new Genre { Id = new Guid("91b08449-4675-40b5-90bb-b0f9cb7f05ab"), Name = "Massive Multiplayer Online"},
        new Genre { Id = new Guid("28b7722e-aa09-4bf7-ac06-d8632b50f5dd"), Name = "Kids and Family"},
        new Genre { Id = new Guid("1eb958cf-f23d-46ec-8f5c-4b82d21328e3"), Name = "Action Adventure"}
    ];

    private readonly List<Game> games;

    public GameStoreData()
    {
        games =
        [
            new Game {
                Id = Guid.NewGuid(),
                Name = "Street Fighter II",
                Genre = genres[0],
                Price = 19.99M,
                ReleaseDate = new DateOnly(1992, 7, 15),
                Description = "Street Fighter II is a legendary arcade fighting game that revolutionized the genre with its intense one-on-one battles, diverse roster of fighters, and deep combo mechanics. Players compete in fast-paced, skill-based combat, executing special moves and combos to defeat opponents."
            },
            new Game {
                Id = Guid.NewGuid(),
                Name = "Final Fantasy XIV",
                Genre = genres[1],
                Price = 59.99M,
                ReleaseDate = new DateOnly(2010, 9, 30),
                Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) set in the expansive world of Eorzea. Featuring an engaging storyline, deep character customization, and epic large-scale battles, players embark on adventures with friends, complete challenging raids, and explore breathtaking environments."
            },
            new Game {
                Id = Guid.NewGuid(),
                Name = "World of Warcraft",
                Genre = genres[2],
                Price = 49.99M,
                ReleaseDate = new DateOnly(2004, 11, 23),
                Description = "World of Warcraft is a groundbreaking MMORPG that immerses players in the vast world of Azeroth. With rich lore, diverse playable races and classes, and thrilling PvE and PvP gameplay, players engage in epic quests, dungeons, and large-scale battles in an ever-evolving online world."
            }
        ];
    }

    public IEnumerable<Game> GetGames() => games;

    public Game? GetGame(Guid id) => games.Find(game => game.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        games.Add(game);
    }

    public void RemoveGame(Guid id)
    {
        games.RemoveAll(game => game.Id == id);
    }

    public IEnumerable<Genre> GetGenres() => genres;

    public Genre? GetGenre(Guid id) => genres.Find(genre => genre.Id == id);
}
