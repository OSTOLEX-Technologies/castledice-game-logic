namespace castledice_game_logic.GameObjects.Decks;

public class IndividualDecksList : IDecksList
{
    private Dictionary<int, List<PlacementType>> _idToPlacementList;

    public IndividualDecksList(Dictionary<int, List<PlacementType>> idToPlacementList)
    {
        _idToPlacementList = idToPlacementList;
    }

    public List<PlacementType> GetDeck(int playerId)
    {
        if (_idToPlacementList.ContainsKey(playerId))
        {
            return _idToPlacementList[playerId];
        }
        
        return new List<PlacementType>();
    }
}