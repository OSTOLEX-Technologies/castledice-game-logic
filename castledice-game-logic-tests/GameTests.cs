using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.Math;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class GameTests
{
    [Fact]
    public void GetAllPlayers_ShouldReturnCollectionOfPlayers_ThatWereGivenInConstructor()
    {
        var firstPlayer = GetPlayer();
        var secondPlayer = GetPlayer();
        var boardConfig = GetDefaultBoardConfig(firstPlayer, secondPlayer);
        List<Player> players = new List<Player>()
        {
            firstPlayer,
            secondPlayer,
        };
        var game = new Game(players, boardConfig);

        var actualPlayers = game.GetAllPlayers();
        
        Assert.Equal(players, actualPlayers);
    }

    [Fact]
    public void GameConstructor_ShouldThrowArgumentException_IfNotAllPlayersHaveCastles()
    {
        var firstPlayer = GetPlayer();
        var secondPlayer = GetPlayer();
        var players = new List<Player>()
        {
            firstPlayer,
            secondPlayer
        };
        var castlesSpawner = new CastlesSpawner(new Dictionary<Player, Vector2Int>()
        {
            {firstPlayer, new Vector2Int(0, 0)}
        });
        var cellsGenerator = new RectCellsGenerator(10, 10);
        var cellType = CellType.Square;
        var boardConfig = new BoardConfig()
        {
            CellsGenerator = cellsGenerator,
            ContentSpawners = new List<IContentSpawner>() { castlesSpawner },
            CellType = cellType
        };
        
        Assert.Throws<ArgumentException>(() => new Game(players, boardConfig));
    }
    
    
}