namespace castledice_game_logic.ActionPointsLogic;

public class ChangeActionPointsAction
{
    public Player Player { get; }

    public int Amount { get; }

    public ChangeActionPointsAction(Player player, int amount)
    {
        Player = player;
        Amount = amount;
    }

    public IActionSnapshot GetSnapshot()
    {
        return new ChangeActionPointsSnapshot(Player.Id, Amount);
    }
}