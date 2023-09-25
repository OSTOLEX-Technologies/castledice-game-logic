namespace castledice_game_logic.Math;

[Serializable]
public struct Vector2Int
{
    public readonly bool Equals(Vector2Int other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector2Int other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public int X { get; set; }
    public int Y { get; set; }

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