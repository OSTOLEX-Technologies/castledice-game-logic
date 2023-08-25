namespace castledice_game_logic.BoardGeneration.CellsGeneration;

/// <summary>
/// Class responsible for populating board with cells and therefore creating the form of the board.
/// </summary>
public interface ICellsGenerator
{
    public void GenerateCells(Board board);
}