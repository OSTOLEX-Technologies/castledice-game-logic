namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class CoordinateContentSpawner : IContentSpawner
{
    private readonly List<ContentToCoordinate> _contentToCoordinates;

    public CoordinateContentSpawner(List<ContentToCoordinate> contentToCoordinates)
    {
        foreach (var coordinate in contentToCoordinates.Select(c => c.Coordinate))
        {
            if (coordinate.X < 0 || coordinate.Y < 0)
            {
                throw new ArgumentException("Given list contains invalid coordinates: " +
                                            $"({coordinate.X}, {coordinate.Y})");
            }
        }
        _contentToCoordinates = contentToCoordinates;
    }

    public void SpawnContent(Board board)
    {
        foreach (var contentToCoordinate in _contentToCoordinates)
        {
            if (!board.HasCell(contentToCoordinate.Coordinate))
            {
                throw new ArgumentException("Cannot spawn content on given board. Cell is missing on coordinate: " +
                                            $"({contentToCoordinate.Coordinate.X}, {contentToCoordinate.Coordinate.Y})");
            }
            board[contentToCoordinate.Coordinate].AddContent(contentToCoordinate.Content);
        }
    }
}