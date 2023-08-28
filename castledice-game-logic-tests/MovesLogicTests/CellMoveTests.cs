using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class CellMoveTests
{
    [Fact]
    public void CellProperty_ShouldReturnCell_ThatWasGivenInConstructor()
    {
        var cell = GetCell();
        var moveCell = new CellMove(cell, MoveType.Place);
        
        Assert.Same(cell, moveCell.Cell);
    }

    [Fact]
    public void MoveTypeProperty_ShouldReturnMoveType_ThatWasGivenInConstructor()
    {
        var cell = GetCell();
        var moveType = MoveType.Place;
        var moveCell = new CellMove(cell, moveType);
        
        Assert.Equal(moveType, moveCell.MoveType);
    }
}