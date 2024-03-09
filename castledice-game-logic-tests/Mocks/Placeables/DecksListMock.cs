﻿using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Decks;

namespace castledice_game_logic_tests.Mocks;

public class DecksListMock : IDecksList
{
    public List<PlacementType> ListToReturn = new();
    
    public List<PlacementType> GetDeck(int playerId)
    {
        return ListToReturn;
    }
}