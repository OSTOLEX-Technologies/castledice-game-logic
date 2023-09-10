using castledice_game_logic.GameObjects;

namespace castledice_game_logic;

public class GameOverChecker
{
    private Board _board;

    public GameOverChecker(Board board)
    {
        _board = board;
    }

    public Player GetWinner()
    {
        return null;
    }
    
    public bool IsGameOver(){
        return false;
    }

    private List<Player> GetCastlesOwners()
    {
        var castlesOwners = new List<Player>();
        return castlesOwners;
    }

    private List<Castle> GetCastles()
    {
        var castles = new List<Castle>();
        return castles;
    }
}