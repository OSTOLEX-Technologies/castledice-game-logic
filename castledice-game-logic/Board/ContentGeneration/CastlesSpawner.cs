using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.Board.ContentGeneration;

public class CastlesSpawner : IContentSpawner
{
    private Dictionary<Player, Vector2Int> _castlesPlacementsData; 

    public CastlesSpawner(Dictionary<Player, Vector2Int> castlesPlacementsData)
    {
        _castlesPlacementsData = castlesPlacementsData;
    }

    public void SpawnContent(Board board)
    {
        foreach (var placementData in _castlesPlacementsData)
        {
            var castlePosition = placementData.Value;
            var castlePlayer = placementData.Key;
            var cell = board[castlePosition];
            var castle = new Castle(castlePlayer);
            cell.AddContent(castle);
        }
    }
}