namespace castledice_game_logic.ActionPointsLogic;

public class PlayerActionPoints
{
    private int _amount;
    
    public int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Can't set values less than zero!");
            }

            _amount = value;
        }
    }

    public event EventHandler<int> ActionPointsDecreased;
    public event EventHandler<int> ActionPointsIncreased; 

    public void DecreaseActionPoints(int amount)
    {
        if (Amount - amount < 0)
        {
            throw new InvalidOperationException("Can't decrease action points, because they amount will be less than zero!");
        }
        ActionPointsDecreased?.Invoke(this, amount);
        Amount -= amount;
    }

    public void IncreaseActionPoints(int amount)
    {
        ActionPointsIncreased?.Invoke(this, amount);
        Amount += amount;
    }
}