using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.Time;

namespace castledice_game_logic;

public class Player
{
    public PlayerActionPoints ActionPoints { get; }
    public IPlayerTimer Timer { get; }
    
    public int Id { get; }
    
    public virtual bool IsNull => false;

    public Player(PlayerActionPoints actionPoints, IPlayerTimer timer, int id)
    {
        ActionPoints = actionPoints;
        Id = id;
        Timer = timer;
    }
}