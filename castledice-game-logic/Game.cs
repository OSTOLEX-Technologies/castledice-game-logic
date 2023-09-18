using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.Penalties;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic;

/// <summary>
/// This is an entry point for castledice game which provides functionality for initializing game with certain parameters and
/// processing player actions.
/// </summary>
public class Game
{
    public event EventHandler<AbstractMove>? MoveApplied; 
    public event EventHandler<Player>? Win;
    public event EventHandler? Draw;

    private readonly Board _board;
    private readonly UnitBranchesCutter _unitBranchesCutter;
    private readonly BoardUpdater _boardUpdater;

    //Moves logic
    private readonly MoveValidator _moveValidator;
    private readonly MoveApplier _moveApplier;
    private readonly MoveSaver _moveSaver;
    private readonly CellMovesSelector _cellMovesSelector;
    private readonly PossibleMovesSelector _possibleMovesSelector;

    //Actions history
    public ActionsHistory ActionsHistory => _actionsHistory;
    private readonly ActionsHistory _actionsHistory;

    //Action points logic
    private readonly Dictionary<Player, ActionPointsGiver> _actionPointsGivers;
    private readonly GiveActionPointsApplier _giveActionPointsApplier;
    private readonly GiveActionPointsSaver _giveActionPointsSaver;

    //Turns logic
    private readonly PlayersList _players;
    private readonly PlayerTurnsSwitcher _turnsSwitcher;
    private readonly List<ITurnSwitchCondition> _turnSwitchConditions = new();
    
    //Win check
    private readonly GameOverChecker _gameOverChecker;

    //Penalties
    private readonly List<IPenalty> _penalties = new();
    private readonly PlayerKicker _playerKicker;

    public ICurrentPlayerProvider CurrentPlayerProvider => _turnsSwitcher;
    public PlayerTurnsSwitcher TurnsSwitcher => _turnsSwitcher;

    //Events
    public event EventHandler? OnTurnSwitched;

    public Game(List<Player> players,
        BoardConfig boardConfig,
        RandomConfig randomConfig,
        UnitsConfig unitsConfig,
        IPlacementListProvider placementListProvider)
    {
        _players = new PlayersList(players);

        _board = InitializeBoard(boardConfig);
        ValidateBoard();

        _actionsHistory = new ActionsHistory();
        _boardUpdater = new BoardUpdater(_board);
        _turnsSwitcher = new PlayerTurnsSwitcher(_players);
        _unitBranchesCutter = new UnitBranchesCutter(_board);
        _playerKicker = new PlayerKicker(_board);

        _actionPointsGivers = new Dictionary<Player, ActionPointsGiver>();
        foreach (var player in _players)
        {
            var numbersGenerator = new NegentropyRandomNumberGenerator(randomConfig.MinActionPointsRoll,
                randomConfig.MaxActionPointsRoll + 1,
                randomConfig.ProbabilityPrecision);
            _actionPointsGivers.Add(player, new ActionPointsGiver(numbersGenerator, player));
        }

        _giveActionPointsApplier = new GiveActionPointsApplier();
        _giveActionPointsSaver = new GiveActionPointsSaver(_actionsHistory);
        var knightFactory = new KnightsFactory(unitsConfig.KnightConfig);
        IPlaceablesFactory placeablesFactory  = new UnitsFactory(knightFactory);
        _moveApplier = new MoveApplier(_board);
        _moveValidator = new MoveValidator(_board, _turnsSwitcher);
        _moveSaver = new MoveSaver(_actionsHistory);
        _cellMovesSelector = new CellMovesSelector(_board);
        _possibleMovesSelector = new PossibleMovesSelector(_board, placeablesFactory, placementListProvider);

        _gameOverChecker = new GameOverChecker(_board, _turnsSwitcher, _cellMovesSelector);
        
        GiveActionPointsToFirstPlayer();
    }

    private void GiveActionPointsToFirstPlayer()
    {
        var currentPlayer = _turnsSwitcher.GetCurrentPlayer();
        var firstActionPointsGive = _actionPointsGivers[currentPlayer].GiveActionPoints();
        _giveActionPointsApplier.ApplyAction(firstActionPointsGive);
        _giveActionPointsSaver.SaveAction(firstActionPointsGive);
    }

    #region Initialize methods

    private Board InitializeBoard(BoardConfig config)
    {
        var board = new Board(config.CellType);
        config.CellsGenerator.GenerateCells(_board);
        //TODO: Make sure that there is only one castle spawner and it is the first one in the list.
        foreach (var contentGenerator in config.ContentSpawners)
        {
            contentGenerator.SpawnContent(_board);
        }

        return board;
    }

    #endregion

    private void ValidateBoard()
    {
        BoardValidator validator = new BoardValidator();
        if (!validator.BoardCastlesAreValid(_players.ToList(), _board))
        {
            throw new ArgumentException("Castles on the board do not correspond to players list!");
        }
    }

    public List<Player> GetAllPlayers()
    {
        return _players.ToList();
    }

    public Player GetCurrentPlayer()
    {
        return _turnsSwitcher.GetCurrentPlayer();
    }

    public Board GetBoard()
    {
        return _board;
    }

    public List<CellMove> GetCellMoves(Player player)
    {
        return _cellMovesSelector.SelectCellMoves(player);
    }

    public List<AbstractMove> GetPossibleMoves(Player player, Vector2Int position)
    {
        return _possibleMovesSelector.GetPossibleMoves(player, position);
    }

    public void AddPenalty(IPenalty penalty)
    {
        _penalties.Add(penalty);
    }

    public void AddTurnSwitchCondition(ITurnSwitchCondition condition)
    {
        _turnSwitchConditions.Add(condition);
    }

    public bool TryMakeMove(AbstractMove move)
    {
        if (!CanMakeMove(move))
        {
            return false;
        }

        _moveApplier.ApplyMove(move);
        _moveSaver.SaveMove(move);
        OnMoveApplied(move);
        CutUnitBranches();
        if (CheckGameOver())
        {
            ProcessGameOver();
        }

        CheckTurns();
        return true;
    }

    private void OnMoveApplied(AbstractMove move)
    {
        MoveApplied?.Invoke(this, move);
    }

    private void CutUnitBranches()
    {
        foreach (var player in _players)
        {
            _unitBranchesCutter.CutUnconnectedBranches(player);
        }
    }

    public bool CanMakeMove(AbstractMove move)
    {
        return _moveValidator.ValidateMove(move);
    }

    private bool CheckGameOver()
    {
        return _gameOverChecker.IsGameOver();
    }

    private void ProcessGameOver()
    {
        if (_gameOverChecker.IsDraw())
        {
            OnDraw();
        }
        else
        {
            var winner = _gameOverChecker.GetWinner();
            OnWin(winner);
        }
    }

    public void CheckTurns()
    {
        foreach (var condition in _turnSwitchConditions)
        {
            if (condition.ShouldSwitchTurn())
            {
                SwitchTurn();
                OnTurnSwitched?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void SwitchTurn()
    {
        _turnsSwitcher.GetCurrentPlayer().ActionPoints.Amount = 0;
        _turnsSwitcher.SwitchTurn();
        var currentPlayer = _turnsSwitcher.GetCurrentPlayer();
        var giveActionPointsAction = _actionPointsGivers[currentPlayer].GiveActionPoints();
        _giveActionPointsApplier.ApplyAction(giveActionPointsAction);
        _giveActionPointsSaver.SaveAction(giveActionPointsAction);
        _boardUpdater.UpdateBoard();
        ApplyPenalties();
    }

    private void ApplyPenalties()
    {
        foreach (var penalty in _penalties)
        {
            var violators = penalty.GetViolators();
            foreach (var violator in violators)
            {
                _playerKicker.KickFromBoard(violator);
                _players.KickPlayer(violator);
            }
        }
        if (CheckGameOver())
        {
            ProcessGameOver();
        }
    }

    protected virtual void OnWin(Player e)
    {
        Win?.Invoke(this, e);
    }

    protected virtual void OnDraw()
    {
        Draw?.Invoke(this, EventArgs.Empty);
    }
}