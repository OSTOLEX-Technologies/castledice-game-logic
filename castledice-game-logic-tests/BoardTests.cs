using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Exceptions;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using static castledice_game_logic_tests.ObjectCreationUtility;
using CastleGO = castledice_game_logic.GameObjects.Castle;
namespace castledice_game_logic_tests;

public class BoardTests
{
    [Fact]
    public void ShouldImplementIEnumerable()
    {
        var board = new Board(CellType.Square);
        Assert.True(board is IEnumerable<Cell>);
    }

    [Fact]
    public void BoardEnumerator_ShouldNotReturnNulls()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1,1);
        board.AddCell(0, 0);
        
        foreach (var cell in board)
        {
            Assert.NotNull(cell);
        }
    }
    
    [Fact]
    public void GetCellType_ShouldReturnCellType_ThatWasGivenInConstructor()
    {
        var expectedType = CellType.Square;
        var board = new Board(expectedType);

        var actualType = board.GetCellType();
        
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void Indexer_ShouldThrowCellNotFoundException_IfCellWithGivenIndexDoesntExist()
    {
        var board = new Board(CellType.Square);
        Assert.Throws<CellNotFoundException>(() => board[1, 1]);
    }

    [Fact]
    public void AddCell_ShouldThrowArgumentException_IfNegativeIndexGiven()
    {
        var board = new Board(CellType.Square);
        Assert.Throws<ArgumentException>(() => board.AddCell(-1, -1));
    }

    [Fact]
    public void AddCell_ShouldNotAffectCell_IfCellOnGivenIndexAlreadyExists()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var oldCell = board[1, 1];
        
        board.AddCell(1, 1);
        var newCell = board[1, 1];
        
        Assert.Same(oldCell, newCell);
    }

    [Fact]
    public void Indexer_ShouldReturnCell_IfThereIsCellWithGivenIndex()
    {

        var board = new Board(CellType.Square);
        board.AddCell(1, 1);

        var cell = board[1, 1];
        Assert.NotNull(cell);
    }

    [Fact]
    public void AddCell_ShouldExpandBoardBoundaries()
    {
        var board = new Board(CellType.Square);
        Assert.Throws<CellNotFoundException>(() => board[0, 0]);
        
        board.AddCell(1, 1);
        Assert.NotNull(board[1, 1]);
        Assert.Throws<CellNotFoundException>(() => board[2, 2]);
        board.AddCell(2, 2);
        Assert.NotNull(board[2, 2]);
    }

    [Fact]
    public void HasCell_ShouldReturnFalse_IfIndexIsOutsideOfBoardBoundaries()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.False(board.HasCell(3, 3));
    }

    [Fact]
    public void HasCell_ShouldReturnFalse_IfIndexIsNegative()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.False(board.HasCell(-1, 3));
    }
    
    [Fact]
    public void HasCell_ShouldReturnFalse_IfCellIsNull()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.False(board.HasCell(1, 1));
    }

    [Fact]
    public void HasCell_ShouldReturnTrue_IfCellWithGivenIndexExists()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.True(board.HasCell(2, 2));
    }

    [Fact]
    public void GetCellsPositions_ShouldReturnEmptyList_IfNoCellsSatisfyGivenCondition()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        board.AddCell(1, 1);
        board[1, 1].AddContent(GetObstacle());//Obstacle is definitely not capturable
        
        Assert.Empty(board.GetCellsPositions(cl => cl.HasContent(ct => ct is ICapturable)));
    }

    [Fact]
    public void GetCellsPositions_ShouldReturnAppropriatePositions()
    {
        var tree = new CapturableMock();
        var treePosition = new Vector2Int(9, 9);
        var board = new Board(CellType.Square);
        board.AddCell(treePosition);
        board[treePosition].AddContent(tree);

        var cellPositions = board.GetCellsPositions(c => c.HasContent(ct => ct is ICapturable));
        Assert.Contains(treePosition, cellPositions);
    }
}