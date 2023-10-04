namespace castledice_game_logic.BoardGeneration.CellsGeneration;

/// <summary>
/// <inheritdoc />
/// This one generates cells according to the given presence matrix: two dimensional boolean array where true means that there is cell on this place and false means that there is no cell on this place.
/// Place in this context is a combination of indices in the matrix.
/// </summary>
public class MatrixCellsGenerator : ICellsGenerator
{
    private readonly bool[,] _presenceMatrix;

    public MatrixCellsGenerator(bool[,] presenceMatrix)
    {
        _presenceMatrix = presenceMatrix;
    }
    
    public void GenerateCells(Board board)
    {
        for (int i = 0; i < _presenceMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < _presenceMatrix.GetLength(1); j++)
            {
                if (_presenceMatrix[i, j])
                {
                    board.AddCell(i, j);
                }
            }
        }
    }
}