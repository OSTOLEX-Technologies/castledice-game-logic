using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace castledice_game_logic_tests;

public class MoveCellTests
{
    [Fact]
    public void CellProperty_ShouldReturnCell_ThatWasGivenInConstructor()
    {
        var cell = GetCell();
        var moveCell = new MoveCell(cell, MoveType.Place);
        
        Assert.Same(cell, moveCell.Cell);
    }

    [Fact]
    public void MoveTypeProperty_ShouldReturnMoveType_ThatWasGivenInConstructor()
    {
        var cell = GetCell();
        var moveType = MoveType.Place;
        var moveCell = new MoveCell(cell, moveType);
        
        Assert.Equal(moveType, moveCell.MoveType);
    }
    
    private Cell GetCell()
    {
        return new Cell(0, 0);
    }
}