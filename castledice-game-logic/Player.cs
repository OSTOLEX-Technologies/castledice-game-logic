using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic;

public class Player
{
    public PlayerActionPoints ActionPoints { get; }
    
    public int Id { get; }
    
    public virtual bool IsNull => false;

    public Player(PlayerActionPoints actionPoints, int id)
    {
        ActionPoints = actionPoints;
        Id = id;
    }
}