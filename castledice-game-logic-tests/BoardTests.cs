using castledice_game_logic;
using castledice_game_logic.Board;
using castledice_game_logic.Exceptions;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

using CastleGO = castledice_game_logic.GameObjects.Castle;
namespace castledice_game_logic_tests;

public class BoardTests
{
    [Fact]
    public void TestIfBoardImplementsIEnumerableInterface()
    {
        var board = new Board(CellType.Square);
        Assert.True(board is IEnumerable<Cell>);
    }

    [Fact]
    public void TestIfBoardEnumeratorDoesntReturnNulls()
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
    public void TestGetCellTypeReturnsGivenCellType()
    {
        var expectedType = CellType.Square;
        var board = new Board(expectedType);

        var actualType = board.GetCellType();
        
        Assert.Equal(expectedType, actualType);
    }

    [Fact]
    public void TestIndexerThrowsCellNotFoundExceptionIfThereNoCellWithGivenIndex()
    {
        var board = new Board(CellType.Square);
        Assert.Throws<CellNotFoundException>(() => board[1, 1]);
    }

    [Fact]
    public void TestAddCellThrowsIndexOutOfRangeExceptionIfIndexIsNegative()
    {
        var board = new Board(CellType.Square);
        Assert.Throws<IndexOutOfRangeException>(() => board.AddCell(-1, -1));
    }

    [Fact]
    public void TestAddCellDoesntChangeCellIfCellAlreadyExists()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        var oldCell = board[1, 1];
        
        board.AddCell(1, 1);
        var newCell = board[1, 1];
        
        Assert.Same(oldCell, newCell);
    }

    [Fact]
    public void TestIndexerReturnsCellIfThereIsCellWithGivenIndex()
    {

        var board = new Board(CellType.Square);
        board.AddCell(1, 1);

        var cell = board[1, 1];
        Assert.NotNull(cell);
    }

    [Fact]
    public void TestAddCellExpandsBoard()
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
    public void TestHasCellReturnsFalseIfIndexIsBiggerThanBoardCurrentSize()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.False(board.HasCell(3, 3));
    }
    
    [Fact]
    public void TestHasCellReturnsFalseIfCellIsNull()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.False(board.HasCell(1, 1));
    }

    [Fact]
    public void TestHasCellReturnsTrueIfCellWithSuchIndexExists()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        
        Assert.True(board.HasCell(2, 2));
    }

    [Fact]
    public void TestGetCellsPositionsReturnsEmptyListIfNoCellsFound()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        board.AddCell(1, 1);
        board[1, 1].AddContent(new Tree());
        
        Assert.Empty(board.GetCellsPositions(cl => cl.HasContent(ct => ct is CastleGO)));
    }

    [Fact]
    public void TestGetCellsPositionReturnsRightPositionsIfCellsAreFound()
    {
        var tree = new Tree();
        var treePosition = new Vector2Int(9, 9);
        var board = new Board(CellType.Square);
        board.AddCell(treePosition);
        board[treePosition].AddContent(tree);

        var cellPositions = board.GetCellsPositions(c => c.HasContent(ct => ct is Tree));
        Assert.Contains(treePosition, cellPositions);
    }
}