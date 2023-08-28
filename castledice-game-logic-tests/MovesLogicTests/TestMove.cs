using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests;

public class TestMove : AbstractMove
{
    public TestMove(Player player, Vector2Int position) : base(player, position)
    {
    }
}
