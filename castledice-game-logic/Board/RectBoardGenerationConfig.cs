using castledice_game_logic.Math;

namespace castledice_game_logic.Board;

public struct RectBoardGenerationConfig
{
    public int Width;
    public int Height;
    public Dictionary<int, Vector2Int> CastleCoordinates; //Here key is a player`s id 
}