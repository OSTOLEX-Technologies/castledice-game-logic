﻿using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;

namespace castledice_game_logic.BoardGeneration.ContentGeneration;

public class CastlesSpawner : IContentSpawner
{
    private readonly Dictionary<Player, Vector2Int> _castlesPlacementsData;
    private readonly ICastlesFactory _factory;

    public CastlesSpawner(Dictionary<Player, Vector2Int> castlesPlacementsData, ICastlesFactory factory)
    {
        _castlesPlacementsData = castlesPlacementsData;
        _factory = factory;
    }

    public void SpawnContent(Board board)
    {
        foreach (var placementData in _castlesPlacementsData)
        {
            var castlePosition = placementData.Value;
            var castlePlayer = placementData.Key;
            var cell = board[castlePosition];
            var castle = _factory.GetCastle(castlePlayer);
            cell.AddContent(castle);
        }
    }
}