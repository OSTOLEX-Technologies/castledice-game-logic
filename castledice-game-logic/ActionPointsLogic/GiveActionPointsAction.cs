namespace castledice_game_logic.ActionPointsLogic;

public class GiveActionPointsAction
{
    public Player Player { get; }

    public int Amount { get; }

    public GiveActionPointsAction(Player player, int amount)
    {
        Player = player;
        if (amount < 0)
        {
            throw new ArgumentException("Amount to give must be bigger than 0.");
        }
        Amount = amount;
    }

    public IActionSnapshot GetSnapshot()
    {
        return new GiveActionPointsSnapshot(Player.Id, Amount);
    }
}