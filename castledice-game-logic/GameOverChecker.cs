using castledice_game_logic.GameObjects;

namespace castledice_game_logic;

public class GameOverChecker
{
    private readonly Board _board;

    public GameOverChecker(Board board)
    {
        _board = board;
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
    
    public bool IsGameOver(){
        return GetCastlesOwners().Count == 1;
    }

    private List<Player> GetCastlesOwners()
    {
        var castlesOwners = new HashSet<Player>();
        var castles = GetCastles();
        foreach (var castle in castles)
        {
            castlesOwners.Add(castle.GetOwner());
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