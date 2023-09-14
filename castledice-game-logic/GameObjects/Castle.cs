namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    private int _durability;
    private readonly int _defaultDurability;
    private readonly int _freeDurability; //Durability of the castle that has no owner.
    
    /// <summary>
    /// Parameters durability and freeDurability must be positive. Otherwise exception will be thrown.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="durability"></param>
    /// <param name="freeDurability"></param>
    /// <exception cref="ArgumentException"></exception>
    public Castle(Player player, int durability, int freeDurability)
    {
        if (durability <= 0)
        {
            throw new ArgumentException("Durability must be positive!");
        }
        if (freeDurability <= 0)
        {
            throw new ArgumentException("Free durability must be positive!");
        }
        _player = player;
        _durability = durability;
        _defaultDurability = durability;
        _freeDurability = freeDurability;
    }

    public void Capture(Player capturer)
    {
        if (capturer == _player)
        {
            return;
        }
        int capturerActionPoints = capturer.ActionPoints.Amount;
        if (capturerActionPoints < _durability)
        {
            capturer.ActionPoints.DecreaseActionPoints(capturerActionPoints);
            _durability -= capturerActionPoints;
        }
        else
        {
            capturer.ActionPoints.DecreaseActionPoints(_durability);
            _durability = 0;
            _player = capturer;
            _durability = _defaultDurability;
        }
    }

    public bool CanBeCaptured(Player capturer)
    {
        if (capturer == _player)
        {
            return false;
        }
        return capturer.ActionPoints.Amount > 0;
    }

    public int GetCaptureCost(Player capturer)
    {
        int capturerActionPoints = capturer.ActionPoints.Amount;
        if (capturerActionPoints < _durability)
        {
            return capturerActionPoints;
        }
        return _durability;
    }

    public void Free()
    {
        _player = new NullPlayer();
        _durability = _freeDurability;
    }

    public Player GetOwner()
    {
        return _player;
    }

    public bool IsBlocking()
    {
        return true;
    }

    public override void Update()
    {
        
    }

    public override void Accept(IContentVisitor visitor)
    {
        visitor.VisitCastle(this);
    }
}
