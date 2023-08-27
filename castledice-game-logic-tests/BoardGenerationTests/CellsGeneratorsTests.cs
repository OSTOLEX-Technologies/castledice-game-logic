using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace castledice_game_logic_tests;

public class CellsGeneratorsTests
{
    [Fact]
    public void GenerateCells_ShouldCreateCells_InNByMRectangleForm()
    {
        int boardWidth = 10;
        int boardLength = 15;
        var board = new Board(CellType.Square);
        var cellsGenerator = new RectCellsGenerator(boardLength, boardWidth);
        
        cellsGenerator.GenerateCells(board);

        for (int i = 0; i < boardLength; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                Assert.True(board[i, j] != null);
            }
        }
    }
}