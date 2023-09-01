namespace castledice_game_logic.Math;

public struct Vector2Int
{
    public int X;
    public int Y;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Vector2Int first, Vector2Int second)
    {
        return first.X == second.X && first.Y == second.Y;
    }
    
    public static bool operator !=(Vector2Int first, Vector2Int second)
    {
        return first.X != second.X || first.Y != second.Y;
    }

    public static implicit operator Vector2Int((int, int) tuple)
    {
        return new Vector2Int(tuple.Item1, tuple.Item2);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}