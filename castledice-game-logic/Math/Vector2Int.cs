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
}