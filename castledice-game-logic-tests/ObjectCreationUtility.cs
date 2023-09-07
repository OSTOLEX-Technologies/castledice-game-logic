using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;

public static class ObjectCreationUtility
{
    public static CastleGO GetCastle(Player player)
    {
        return new CastleGO(player);
    }
    
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
    
    public static Player GetPlayer(int id = 0, int actionPoints = 6)
    {
        var playerActionPoints = new PlayerActionPoints
        {
            Amount = actionPoints,
        };
        var playerId = id;
        return new Player(playerActionPoints, playerId);
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
        return new ObstacleMock();
    }

    public static IPlaceable GetPlaceable()
    {
        return new PlaceableMock();
    }

    public abstract class AbstractMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
    }
    
    public class PlaceMoveBuilder : AbstractMoveBuilder
    {

        public IPlaceable Content = GetPlaceable();
        
        public PlaceMove Build()
        {
            return new PlaceMove(Player, Position, Content);
        }
    }
    
    public class ReplaceMoveBuilder : AbstractMoveBuilder
    {
        public IPlaceable Replacement = GetPlaceable();
        
        public ReplaceMove Build()
        {
            return new ReplaceMove(Player, Position, Replacement);
        }
    }

    public class RemoveMoveBuilder : AbstractMoveBuilder
    {
        public RemoveMove Build()
        {
            return new RemoveMove(Player, Position);
        }
    }

    public class UpgradeMoveBuilder : AbstractMoveBuilder
    {
        public UpgradeMove Build()
        {
            return new UpgradeMove(Player, Position);
        }
    }

    public class CaptureMoveBuilder : AbstractMoveBuilder
    {
        public CaptureMove Build()
        {
            return new CaptureMove(Player, Position);
        }
    }

    public class TestMoveBuilder : AbstractMoveBuilder
    {
        public TestMove Build()
        {
            return new TestMove(Player, Position);
        }
    }

    public class PossibleMovesSelectorBuilder
    {
        public Board Board = GetFullNByNBoard(3);
        public IPlaceablesFactory PlaceablesFactory = new PlaceableMocksFactory();
        public IPlacementListProvider PlacementListProvider = new PlacementListProviderMock()
        {
            ListToReturn = new List<PlacementType>() { PlacementType.Knight , PlacementType.HeavyKnight}
        };

        public PossibleMovesSelector Build()
        {
            return new PossibleMovesSelector(Board, PlaceablesFactory, PlacementListProvider);
        }
    }
}