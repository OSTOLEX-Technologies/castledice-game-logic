namespace castledice_game_logic.GameObjects.Configs;

public struct TreeConfig
{
    public int RemoveCost { get; }
    public bool CanBeRemoved { get; }

    public TreeConfig(int removeCost, bool canBeRemoved)
    {
        RemoveCost = removeCost;
        CanBeRemoved = canBeRemoved;
    }
}