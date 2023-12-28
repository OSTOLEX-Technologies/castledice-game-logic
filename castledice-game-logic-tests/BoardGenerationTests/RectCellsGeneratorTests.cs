using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace castledice_game_logic_tests.BoardGenerationTests;

public class RectCellsGeneratorTests
{
    [Theory]
    [InlineData(10, 10)]
    [InlineData(11, 15)]
    [InlineData(7, 12)]
    [InlineData(8, 4)]
    public void GenerateCells_ShouldCreateCells_InNByMRectangleForm(int boardWidth, int boardLength)
    {
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(boardLength, boardWidth);
        
        cellsGenerator.GenerateCells(board);

        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                Assert.True(board.HasCell(i, j));
            }
        }
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(10, 20)]
    [InlineData(100, 30)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int boardWidth, int boardLength)
    {
        var generator = new RectCellsGenerator(boardLength, boardWidth);
        
        Assert.Equal(boardLength, generator.BoardLength);
        Assert.Equal(boardWidth, generator.BoardWidth);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfGivenBoardLengthIsLessThanOne(int boardLength)
    {
        Assert.Throws<ArgumentException>(() => new RectCellsGenerator(boardLength, 10));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfGivenBoardWidthIsLessThanOne(int boardWidth)
    {
        Assert.Throws<ArgumentException>(() => new RectCellsGenerator(10, boardWidth));
    }
}