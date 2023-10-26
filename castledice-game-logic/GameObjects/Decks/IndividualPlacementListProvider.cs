namespace castledice_game_logic.GameObjects.Decks;

public class IndividualPlacementListProvider : IPlacementListProvider
{
    private Dictionary<int, List<PlacementType>> _idToPlacementList;

    public IndividualPlacementListProvider(Dictionary<int, List<PlacementType>> idToPlacementList)
    {
        _idToPlacementList = idToPlacementList;
    }

    public List<PlacementType> GetPlacementList(int playerId)
    {
        if (_idToPlacementList.ContainsKey(playerId))
        {
            return _idToPlacementList[playerId];
        }
        
        return new List<PlacementType>();
    }
}