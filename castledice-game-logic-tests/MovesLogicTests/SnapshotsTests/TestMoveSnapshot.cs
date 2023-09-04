using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic_tests.SnapshotsTests;

public class TestMoveSnapshot : AbstractMoveSnapshot
{
    public TestMoveSnapshot(AbstractMove move) : base(move)
    {
    }

    public override MoveType MoveType { get;  }
    
    public override string GetJson()
    {
        throw new NotImplementedException();
    }
}