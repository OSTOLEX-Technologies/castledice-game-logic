using castledice_game_logic.Board;
using castledice_game_logic.Board.CellsGeneration;

namespace castledice_game_logic_tests;

public class CellsGeneratorsTests
{
    [Fact]
    public void RectCellsGeneratorGeneratesCellsInNByMArea()
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