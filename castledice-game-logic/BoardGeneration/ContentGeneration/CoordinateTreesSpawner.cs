using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class CoordinateTreesSpawner : IContentSpawner
{
    private readonly List<Vector2Int> _coordinates;
    private readonly ITreesFactory _factory;
    
    public CoordinateTreesSpawner(List<Vector2Int> coordinates, ITreesFactory treesFactory)
    {
        if (CoordinatesListHasDuplicates(coordinates))
        {
            throw new ArgumentException("Coordinate list must not contain duplicates.");
        }
        if (CoordinatesListHasNegativeCoordinates(coordinates))
        {
            throw new ArgumentException("All coordinates must be non-negative.");
        }

        _coordinates = coordinates;
        _factory = treesFactory;
    }

    private static bool CoordinatesListHasDuplicates(List<Vector2Int> coordinates)
    {
        for (int i = 0; i < coordinates.Count; i++)
        {
            var firstCoordinate = coordinates[i];
            for (int j = i; j < coordinates.Count; j++)
            {
                if (j == i)
                {
                    continue;
                }

                var secondCoordinate = coordinates[j];
                if (firstCoordinate == secondCoordinate)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool CoordinatesListHasNegativeCoordinates(List<Vector2Int> coordinates)
    {
        return coordinates.Exists(cord => cord.X < 0 || cord.Y < 0);
    }

    public void SpawnContent(Board board)
    {
        foreach (var coordinate in _coordinates)
        {
            if (!board.HasCell(coordinate))
            {
                throw new ArgumentException("Board doesn't have cell on coordinate " + coordinate);
            }
        }
        foreach (var coordinate in _coordinates)
        {
            board[coordinate].AddContent(_factory.GetTree());
        }
    }
}