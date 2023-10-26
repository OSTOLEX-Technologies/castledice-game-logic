using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class ContentToCoordinate
{
    public Vector2Int Coordinate { get; }
    public Content Content { get; }

    public ContentToCoordinate(Vector2Int coordinate, Content content)
    {
        Coordinate = coordinate;
        Content = content;
    }
}