using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests;

public static class ObjectCreationUtility
{
    public static Board GetFullNByNBoard(int size)
    {
        var board = new Board(CellType.Square);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board.AddCell(i, j);
            }
        }
        return board;
    }
    
    /// <summary>
    /// Returns cell with (0, 0) coordinates.
    /// </summary>
    /// <returns></returns>
    public static Cell GetCell()
    {
        return new Cell(new Vector2Int(0, 0));
    }
    
    public static List<Player> GetPlayersList(int length)
    {
        List<Player> players = new List<Player>();
        for (int i = 0; i < length; i++)
        {
            players.Add(GetPlayer());
        }

        return players;
    }

    public static BoardConfig GetDefaultBoardConfig(Player firstPlayer, Player secondPlayer)
    {
        var cellsGenerator = new RectCellsGenerator(10, 10);
        var castlesSpawner = new CastlesSpawner(new Dictionary<Player, Vector2Int>()
        {
            {firstPlayer, new Vector2Int(0, 0)},
            {secondPlayer, new Vector2Int(9, 9)}
        });
        var cellType = CellType.Square;
        var boardConfig = new BoardConfig()
        {
            CellsGenerator = cellsGenerator,
            ContentSpawners = new List<IContentSpawner>()
            {
                castlesSpawner
            },
            CellType = cellType
        };
        return boardConfig;
    }
    
    
    /// <summary>
    /// Returns player object with 6 action points.
    /// </summary>
    /// <returns></returns>
    public static Player GetPlayer()
    {
        return GetPlayer(6);
    }

    /// <summary>
    /// Returns player object with given amount of action points.
    /// </summary>
    /// <returns></returns>
    public static Player GetPlayer(int actionPoints)
    {
        var playerActionPoints = new PlayerActionPoints
        {
            Amount = actionPoints,
        };
        return new Player(playerActionPoints);
    }

    
    /// <summary>
    /// Returns some cell content that is easy to create. You should not expect any particular content.
    /// </summary>
    /// <returns></returns>
    public static Content GetCellContent()
    {
        return new ObstacleMock();
    }

    
    /// <summary>
    /// Returns cell content that can't be removed by player.
    /// </summary>
    /// <returns></returns>
    public static Content GetObstacle()
    {
        return new Tree();
    }
    
    public class PlaceMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
        public Content Content = GetCellContent();
        
        public PlaceMove Build()
        {
            return new PlaceMove(Player, Position, Content);
        }
    }
    
    public class RemoveMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
        public Content Replacement = GetCellContent();
        
        public RemoveMove Build()
        {
            return new RemoveMove(Player, Position, Replacement);
        }
    }

    public class UpgradeMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
        
        public UpgradeMove Build()
        {
            return new UpgradeMove(Player, Position);
        }
    }
    
    public class CaptureMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
        
        public CaptureMove Build()
        {
            return new CaptureMove(Player, Position);
        }
    }
}