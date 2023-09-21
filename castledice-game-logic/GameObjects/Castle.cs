namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    private int _durability;
    private readonly int _captureCost;
    private readonly int _defaultDurability;
    private readonly int _freeDurability; //Durability of the castle that has no owner.

    /// <summary>
    /// Parameters durability and freeDurability must be positive. Otherwise exception will be thrown.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="durability"></param>
    /// <param name="freeDurability"></param>
    /// <param name="captureCost"></param>
    /// <exception cref="ArgumentException"></exception>
    public Castle(Player player, int durability, int freeDurability, int captureCost)
    {
        if (durability <= 0)
        {
            throw new ArgumentException("Durability must be positive!");
        }
        if (freeDurability <= 0)
        {
            throw new ArgumentException("Free durability must be positive!");
        }
        if (captureCost <= 0)
        {
            throw new ArgumentException("Capture cost must be positive!");
        }
        _player = player;
        _durability = durability;
        _defaultDurability = durability;
        _freeDurability = freeDurability;
        _captureCost = captureCost;
    }

    public int GetDurability()
    {
        return _durability;
    }

    public void Capture(Player capturer)
    {
        if (capturer == _player)
        {
            return;
        }
        int captureCost = GetCaptureCost(capturer);
        if (capturer.ActionPoints.Amount < captureCost)
        {
            return;
        }
        capturer.ActionPoints.DecreaseActionPoints(captureCost);
        _durability -= captureCost;
        if (_durability > 0) return;
        _player = capturer;
        _durability = _defaultDurability;
    }

    public bool CanBeCaptured(Player capturer)
    {
        if (capturer == _player)
        {
            return false;
        }
        return capturer.ActionPoints.Amount >= GetCaptureCost(capturer);
    }

    public int GetCaptureCost(Player capturer)
    {
        return _captureCost > _durability ? _durability : _captureCost;
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

    public override T Accept<T>(IContentVisitor<T> visitor)
    {
        return visitor.VisitCastle(this);
    }
}
