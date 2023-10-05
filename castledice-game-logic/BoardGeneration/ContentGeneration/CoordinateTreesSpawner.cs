using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class CoordinateTreesSpawner : IContentSpawner
{
    public CoordinateTreesSpawner(List<Vector2Int> coordinates, ITreesFactory treesFactory)
    {
        
    }

    public void SpawnContent(Board board)
    {
        throw new NotImplementedException();
    }
}