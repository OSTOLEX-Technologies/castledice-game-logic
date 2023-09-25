using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic;

public class GameOverChecker
{
    private readonly Board _board;
    private readonly ICurrentPlayerProvider _currentPlayerProvider;
    private readonly CellMovesSelector _cellMovesSelector;

    public GameOverChecker(Board board, ICurrentPlayerProvider currentPlayerProvider, CellMovesSelector cellMovesSelector)
    {
        _board = board;
        _currentPlayerProvider = currentPlayerProvider;
        _cellMovesSelector = cellMovesSelector;
    }


    
    public bool IsGameOver(){
        return !GetWinner().IsNull || IsDraw();
    }
    
    public Player GetWinner()
    {
        var castlesOwners = GetCastlesOwners();
        if (castlesOwners.Count == 1)
        {
            return castlesOwners[0];
        }
        return new NullPlayer();
    }
    
    public bool IsDraw()
    {
        var currentPlayer = _currentPlayerProvider.GetCurrentPlayer();
        var currentPlayerMoves = _cellMovesSelector.SelectCellMoves(currentPlayer);
        var currentPlayerActionPoints = currentPlayer.ActionPoints.Amount;
        return currentPlayerActionPoints > 0 && currentPlayerMoves.Count == 0;
    }
    
    private List<Player> GetCastlesOwners()
    {
        var castlesOwners = new HashSet<Player>();
        var castles = GetCastles();
        foreach (var castle in castles)
        {
            if (!castle.GetOwner().IsNull)
            {
                castlesOwners.Add(castle.GetOwner());
            }
        }
        return castlesOwners.ToList();
    }

    private List<Castle> GetCastles()
    {
        var castles = new List<Castle>();
        foreach (var cell in _board)
        {
            foreach (var content in cell.GetContent())
            {
                if (content is Castle castle)
                {
                    castles.Add(castle);
                }
            }
        }
        return castles;
    }
}