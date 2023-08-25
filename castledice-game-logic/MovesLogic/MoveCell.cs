namespace castledice_game_logic.MovesLogic;

public class MoveCell
{
    private Cell _cell;
    private MoveType _type;

    public Cell Cell => _cell;
    public MoveType MoveType => _type;

    public MoveCell(Cell cell, MoveType type)
    {
        _cell = cell;
        _type = type;
    }
}