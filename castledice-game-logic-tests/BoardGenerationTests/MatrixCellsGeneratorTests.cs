using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace castledice_game_logic_tests;

public class MatrixCellsGeneratorTests
{
    [Theory]
    [MemberData(nameof(MatrixCases))]
    public void GenerateCells_ShouldSpawnCells_AccordingToMatrix(bool[,] matrix)
    {
        var board = new Board(CellType.Square);
        var generator = new MatrixCellsGenerator(matrix);
        
        generator.GenerateCells(board);

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Assert.Equal(matrix[i, j], board.HasCell(i, j));
            }
        }
    }

    public static IEnumerable<object[]> MatrixCases()
    {
        yield return new object[]
        {
            new bool[,]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true, }
            }
        };
        yield return new object[]
        {
            new bool[,]
            {
                { false, true, false },
                { true, true, true },
                { false, true, false, }
            }
        };
        yield return new object[]
        {
            new bool[,]
            {
                { false, false, false },
                { false, false, false },
                { false, false, false, }
            }
        };
    }
}