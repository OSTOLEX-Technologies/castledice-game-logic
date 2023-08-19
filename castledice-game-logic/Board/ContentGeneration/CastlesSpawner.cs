using castledice_game_logic.Math;

namespace castledice_game_logic.Board.ContentGeneration;

public class CastlesSpawner : IContentSpawner
{
    private Dictionary<int, Vector2Int> _castlesCoordinates; //Here key is a player`s id

    public CastlesSpawner(Dictionary<int, Vector2Int> castlesCoordinates)
    {
        _castlesCoordinates = castlesCoordinates;
    }

    public void SpawnContent(Board board)
    {
        
    }
}