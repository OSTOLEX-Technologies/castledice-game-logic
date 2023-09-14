﻿using castledice_game_logic.ActionPointsLogic;
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
    public event EventHandler<Player>? GameOver; 
    
    private Board _board;
    private UnitBranchesCutter _unitBranchesCutter;
    private BoardUpdater _boardUpdater;
    
    //Moves logic
    private MoveValidator _moveValidator;
    private MoveApplier _moveApplier;
    private MoveSaver _moveSaver;
    private CellMovesSelector _cellMovesSelector;
    private PossibleMovesSelector _possibleMovesSelector;

    //Actions history
    private ActionsHistory _actionsHistory;
    
    //Action points logic
    private Dictionary<Player, ActionPointsGiver> _actionPointsGivers;
    private GiveActionPointsApplier _giveActionPointsApplier;
    private GiveActionPointsSaver _giveActionPointsSaver;
    
    //Turns logic
    private PlayersList _players;
    private PlayerTurnsSwitcher _turnsSwitcher;
    private List<ITurnSwitchCondition> _turnSwitchConditions;
    
    //Factories
    private IPlaceablesFactory _placeablesFactory;
    
    //GameOver check
    private GameOverChecker _gameOverChecker;
    
    //Penalties
    private List<IPenalty> _penalties;
    

    public Game(List<Player> players, 
        BoardConfig boardConfig, 
        List<ITurnSwitchCondition> turnSwitchConditions, 
        RandomConfig randomConfig, 
        UnitsConfig unitsConfig, 
        IPlacementListProvider placementListProvider,
        List<IPenalty> penalties)
    {
        InitializePlayers(players);
        InitializeTurns(turnSwitchConditions);
        InitializeHistory();
        InitializeActionPoints(randomConfig);
        InitializePlaceablesFactory(unitsConfig);
        InitializeMovesLogic(placementListProvider);
        InitializeGameOverCheck();
        InitializeBranchesCutter();
        InitializeBoardUpdater();
        InitializePenatlites(penalties);
        InitializeBoard(boardConfig);
        ValidateBoard();
    }

    private void InitializePenatlites(List<IPenalty> penalties)
    {
        _penalties = penalties;
    }

    private void InitializeBoardUpdater()
    {
        _boardUpdater = new BoardUpdater(_board);

    }

    private void InitializeBranchesCutter()
    {
        _unitBranchesCutter = new UnitBranchesCutter(_board);

    }

    private void InitializePlayers(List<Player> players)
    {
        _players = new PlayersList(players);
    }

    private void InitializeTurns(List<ITurnSwitchCondition> turnSwitchConditions)
    {
        _turnsSwitcher = new PlayerTurnsSwitcher(_players);
        _turnSwitchConditions = turnSwitchConditions;
    }

    private void InitializeHistory()
    {
        _actionsHistory = new ActionsHistory();
    }

    private void InitializeActionPoints(RandomConfig randomConfig)
    {
        _actionPointsGivers = new Dictionary<Player, ActionPointsGiver>();
        foreach (var player in _players)
        {
            var numbersGenerator = new NegentropyRandomNumberGenerator(randomConfig.MinActionPointsRoll,
                randomConfig.MaxActionPointsRoll, 
                randomConfig.ProbabilityPrecision);
            _actionPointsGivers.Add(player, new ActionPointsGiver(numbersGenerator, player));
        }

        _giveActionPointsApplier = new GiveActionPointsApplier();
        _giveActionPointsSaver = new GiveActionPointsSaver(_actionsHistory);
    }

    private void InitializePlaceablesFactory(UnitsConfig config)
    {
        var knightFactory = new KnightsFactory(config.KnightConfig);
        _placeablesFactory = new UnitsFactory(knightFactory);
    }
    
    private void InitializeMovesLogic(IPlacementListProvider placementListProvider)
    {
        _moveApplier = new MoveApplier(_board);
        _moveValidator = new MoveValidator(_board, _turnsSwitcher);
        _moveSaver = new MoveSaver(_actionsHistory);
        _cellMovesSelector = new CellMovesSelector(_board);
        _possibleMovesSelector = new PossibleMovesSelector(_board, _placeablesFactory, placementListProvider);
    }

    private void InitializeGameOverCheck()
    {
        _gameOverChecker = new GameOverChecker(_board);
    }

    private void InitializeBoard(BoardConfig config)
    {
        _board = new Board(config.CellType);
        config.CellsGenerator.GenerateCells(_board);
        //TODO: Make sure that there is only one castle spawner and it is the first one in the list.
        foreach (var contentGenerator in config.ContentSpawners)
        {
            contentGenerator.SpawnContent(_board);
        }
    }
    

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
    
    public bool TryMakeMove(AbstractMove move)
    {
        if (!CanMakeMove(move))
        {
            return false;
        }
        _moveApplier.ApplyMove(move);
        _moveSaver.SaveMove(move);
        CutUnitBranches();
        if (CheckGameOver())
        {
            ProcessGameOver();
        }
        CheckTurns();
        return true;
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
        var winner = _gameOverChecker.GetWinner();
        OnGameOver(winner);
    }

    public void CheckTurns()
    {
        foreach (var condition in _turnSwitchConditions)
        {
            if (condition.ShouldSwitchTurn())
            {
                SwitchTurn();
            }
        }
    }

    private void SwitchTurn()
    {
        _turnsSwitcher.SwitchTurn();
        var currentPlayer = _turnsSwitcher.GetCurrentPlayer();
        var giveActionPointsAction = _actionPointsGivers[currentPlayer].GiveActionPoints();
        _giveActionPointsApplier.ApplyAction(giveActionPointsAction);
        _giveActionPointsSaver.SaveAction(giveActionPointsAction);
        _boardUpdater.UpdateBoard();
    }

    protected virtual void OnGameOver(Player e)
    {
        GameOver?.Invoke(this, e);
    }
}