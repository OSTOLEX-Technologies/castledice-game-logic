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

    protected bool Equals(MoveCell other)
    {
        return _cell.Equals(other._cell) && _type == other._type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MoveCell)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_cell, (int)_type);
    }
}