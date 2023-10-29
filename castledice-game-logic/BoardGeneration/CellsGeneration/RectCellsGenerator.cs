namespace castledice_game_logic.BoardGeneration.CellsGeneration;


/// <summary>
/// <inheritdoc />
/// This one generates cells in rectangular form.
/// </summary>
public class RectCellsGenerator : ICellsGenerator
{
    public int BoardLength => _boardLength;
    public int BoardWidth => _boardWidth;
    
    private readonly int _boardLength;
    private readonly int _boardWidth;

    public RectCellsGenerator(int boardLength, int boardWidth)
    {
        _boardLength = boardLength;
        _boardWidth = boardWidth;
    }

    public void GenerateCells(Board board)
    {
        board.AddCell(_boardLength-1, _boardWidth-1);
        for (int i = 0; i < _boardLength; i++)
        {
            for (int j = 0; j < _boardWidth; j++)
            {
                board.AddCell(i, j);
            }
        }
    }
}