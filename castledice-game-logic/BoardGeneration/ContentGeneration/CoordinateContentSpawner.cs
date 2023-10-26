namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class CoordinateContentSpawner : IContentSpawner
{
    private readonly List<ContentToCoordinate> _contentToCoordinates;

    public CoordinateContentSpawner(List<ContentToCoordinate> contentToCoordinates)
    {
        _contentToCoordinates = contentToCoordinates;
    }

    public void SpawnContent(Board board)
    {
        throw new NotImplementedException();
    }
}