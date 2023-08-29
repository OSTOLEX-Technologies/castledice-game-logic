using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic;

public class Player
{
    public PlayerActionPoints ActionPoints { get; }

    public Player(PlayerActionPoints actionPoints)
    {
        ActionPoints = actionPoints;
    }
}