using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic;

/// <summary>
/// This is an entry point for castledice game which provides functionality for initializing game with certain parameters and
/// processing player actions.
/// </summary>
public class Game
{
    public event EventHandler<Player>? GameOver; 
    
    private Board _board;
    
    //Moves logic
    private MoveValidator _moveValidator;
    private MoveApplier _moveApplier;
    private MoveSaver _moveSaver;
    private CellMovesSelector _cellMovesSelector;
    private PossibleMovesSelector _possibleMovesSelector;

    //Actions history
    private ActionsHistory _actionsHistory;
    
    //Action points logic
    private ActionPointsGiver _actionPointsGiver;
    private GiveActionPointsApplier _giveActionPointsApplier;
    private GiveActionPointsSaver _giveActionPointsSaver;
    
    //Turns logic
    private PlayersList _players;
    private PlayerTurnsSwitcher _turnsSwitcher;
    private List<ITurnSwitchCondition> _turnSwitchConditions;
    
    //Factories
    private IPlaceablesFactory _placeablesFactory;
    

    public Game(List<Player> players, BoardConfig boardConfig)
    {
        // _players = players;
        // _board = new Board(boardConfig.CellType);
        // boardConfig.CellsGenerator.GenerateCells(_board);
        // foreach (var contentGenerator in boardConfig.ContentSpawners)
        // {
        //     contentGenerator.SpawnContent(_board);
        // }
        //
        // ValidateBoard();
        
        _players = new PlayersList();
        var firstPlayer = new Player(new PlayerActionPoints(), 1);
        var secondPlayer = new Player(new PlayerActionPoints(), 2);
        _players.AddPlayer(firstPlayer);
        _players.AddPlayer(secondPlayer);
        
        _board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(10, 10);

        
        var castlesPositions = new Dictionary<Player, Vector2Int>()
        {
            { firstPlayer, (0, 0) },
            { secondPlayer, (9, 9) }
        };
        var castleConfig = new CastleConfig(){Durability = 3, FreeDurability = 1};
        var castlesFactory = new CastlesFactory(castleConfig);
        var castlesSpawner = new CastlesSpawner(castlesPositions, castlesFactory);
        
        var treeConfig = new TreeConfig(){CanBeRemoved = false};
        var treesFactory = new TreesFactory(treeConfig);
        var treesSpawner = new TreesSpawner(1, 3, 3, treesFactory);
        
        cellsGenerator.GenerateCells(_board);
        castlesSpawner.SpawnContent(_board);
        treesSpawner.SpawnContent(_board);
        
    }

    // private void ValidateBoard()
    // {
    //     BoardValidator validator = new BoardValidator();
    //     if (!validator.BoardCastlesAreValid(_players, _board))
    //     {
    //         throw new ArgumentException("Castles on the board do not correspond to players list!");
    //     }
    // }

    public List<Player> GetAllPlayers()
    {
        return _players.ToList();
    }

    public Player GetCurrentPlayer()
    {
        throw new NotImplementedException();
    }

    public Board GetBoard()
    {
        throw new NotImplementedException();
    }

    public bool TryMakeMove(AbstractMove move)
    {
        return false;
    }

    public bool CanMakeMove(AbstractMove move)
    {
        return false;
    }

    protected virtual void OnGameOver(Player e)
    {
        GameOver?.Invoke(this, e);
    }
}