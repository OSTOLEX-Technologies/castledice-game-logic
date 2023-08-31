namespace castledice_game_logic.ActionPointsLogic;

public class PlayerActionPoints
{
    public int Amount { get; set; }

    public void DecreaseActionPoints(int amount)
    {
        Amount -= amount;
    }

    public void IncreaseActionPoints(int amount)
    {
        Amount += amount;
    }
}