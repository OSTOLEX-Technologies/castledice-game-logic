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
    public event EventHandler<(Game, Player)>? Win;
    public event EventHandler<Game>? Draw;

    private readonly Board _board;
    private readonly UnitBranchesCutter _unitBranchesCutter;
    private readonly BoardUpdater _boardUpdater;
    private readonly PlaceablesConfig _placeablesConfig;

    //Moves logic
    private readonly MoveValidator _moveValidator;
    private readonly MoveApplier _moveApplier;
    private readonly MoveSaver _moveSaver;
    private readonly CellMovesSelector _cellMovesSelector;
    private readonly PossibleMovesSelector _possibleMovesSelector;
    private readonly MoveCostCalculator _moveCostCalculator;
    private readonly IDecksList _decksList;
    private readonly IPlaceablesFactory _placeablesFactory;

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

    public virtual IPlaceablesFactory PlaceablesFactory => _placeablesFactory;
    public virtual PlaceablesConfig PlaceablesConfig => _placeablesConfig;
    public virtual ICurrentPlayerProvider CurrentPlayerProvider => _turnsSwitcher;
    public virtual PlayerTurnsSwitcher TurnsSwitcher => _turnsSwitcher;

    //Events
    public event EventHandler<Game>? TurnSwitched;

    public Game(List<Player> players,
        BoardConfig boardConfig,
        PlaceablesConfig placeablesConfig,
        IDecksList decksList)
    {
        _players = new PlayersList(players);
        _decksList = decksList;
        _placeablesConfig = placeablesConfig;
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
            _actionPointsGivers.Add(player, new ActionPointsGiver(player));
        }

        _giveActionPointsApplier = new GiveActionPointsApplier();
        _giveActionPointsSaver = new GiveActionPointsSaver(_actionsHistory);
        var knightFactory = new KnightsFactory(placeablesConfig.KnightConfig);
        _placeablesFactory  = new PlaceablesFactory(knightFactory);
        _moveApplier = new MoveApplier(_board);
        _moveValidator = new MoveValidator(_board, _turnsSwitcher);
        _moveSaver = new MoveSaver(_actionsHistory);
        _cellMovesSelector = new CellMovesSelector(_board);
        _possibleMovesSelector = new PossibleMovesSelector(_board, _placeablesFactory, decksList);
        _moveCostCalculator = new MoveCostCalculator(_board);
        
        _gameOverChecker = new GameOverChecker(_board, _turnsSwitcher, _cellMovesSelector);
    }

    #region Initialize methods

    public virtual void GiveActionPointsToPlayer(int playerId, int amount)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId);
        var giveActionPoints = _actionPointsGivers[player].GiveActionPoints(amount);
        _giveActionPointsApplier.ApplyAction(giveActionPoints);
        _giveActionPointsSaver.SaveAction(giveActionPoints);
    }
    
    private static Board InitializeBoard(BoardConfig config)
    {
        var board = new Board(config.CellType);
        config.CellsGenerator.GenerateCells(board);
        foreach (var contentGenerator in config.ContentSpawners)
        {
            contentGenerator.SpawnContent(board);
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

    public virtual List<Player> GetAllPlayers()
    {
        return _players.ToList();
    }

    public virtual List<int> GetAllPlayersIds()
    {
        return _players.Select(p => p.Id).ToList();
    }

    public virtual IDecksList GetDecksList()
    {
        return _decksList;
    }
    
    public virtual Player GetCurrentPlayer()
    {
        return _turnsSwitcher.GetCurrentPlayer();
    }
    
    public virtual Player GetPlayer(int playerId)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId);
        if (player == null)
        {
            throw new ArgumentException("Player with id " + playerId + " does not exist!");
        }
        return player;
    }

    public virtual Board GetBoard()
    {
        return _board;
    }

    public virtual List<CellMove> GetCellMoves(int playerId)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId);
        if (player == null) return new List<CellMove>();
        return _cellMovesSelector.SelectCellMoves(player);
    }

    public virtual List<AbstractMove> GetPossibleMoves(int playerId, Vector2Int position)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId);
        if (player == null) return new List<AbstractMove>();
        return _possibleMovesSelector.GetPossibleMoves(player, position);
    }

    public virtual void AddPenalty(IPenalty penalty)
    {
        _penalties.Add(penalty);
    }

    public virtual void AddTurnSwitchCondition(ITurnSwitchCondition condition)
    {
        _turnSwitchConditions.Add(condition);
    }

    public virtual int GetMoveCost(AbstractMove move)
    {
        return _moveCostCalculator.GetMoveCost(move);
    }
    
    public virtual bool TryMakeMove(AbstractMove move)
    {
        if (!CanMakeMove(move)) return false;
        

        _moveApplier.ApplyMove(move);
        _moveSaver.SaveMove(move);
        OnMoveApplied(move);
        CutUnitBranches();
        if (CheckGameOver())
        {
            ProcessGameOver();
        }
        else
        {
            CheckTurns();
        }
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

    public virtual bool CanMakeMove(AbstractMove move)
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

    public virtual void CheckTurns()
    {
        if (!_turnSwitchConditions.Any(condition => condition.ShouldSwitchTurn())) return;
        SwitchTurn();
        //TODO: Add logic for resetting conditions
    }

    public void SwitchTurn()
    {
        _turnsSwitcher.GetCurrentPlayer().ActionPoints.Amount = 0;
        _turnsSwitcher.SwitchTurn();
        _boardUpdater.UpdateBoard();
        ApplyPenalties();
        TurnSwitched?.Invoke(this, this);
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

    protected void OnWin(Player e)
    {
        Win?.Invoke(this, (this, e));
    }

    protected void OnDraw()
    {
        Draw?.Invoke(this, this);
    }
}