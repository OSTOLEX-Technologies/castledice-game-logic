namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    private int _durability;
    private readonly int _captureHitCost;
    private readonly int _defaultDurability;
    private readonly int _freeDurability; //Durability of the castle that has no owner.

    /// <summary>
    /// Parameters durability and freeDurability must be positive. Otherwise exception will be thrown.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="durability"></param>
    /// <param name="freeDurability"></param>
    /// <param name="captureHitCost"></param>
    /// <exception cref="ArgumentException"></exception>
    public Castle(Player player, int durability, int freeDurability, int captureHitCost)
    {
        if (durability <= 0)
        {
            throw new ArgumentException("Durability must be positive!");
        }

        if (freeDurability <= 0)
        {
            throw new ArgumentException("Free durability must be positive!");
        }

        if (captureHitCost <= 0)
        {
            throw new ArgumentException("CaptureHit cost must be positive!");
        }

        _player = player;
        _durability = durability;
        _defaultDurability = durability;
        _freeDurability = freeDurability;
        _captureHitCost = captureHitCost;
    }

    public int GetMaxDurability()
    {
        if (_player.IsNull)
        {
            return _freeDurability;
        }

        return _defaultDurability;
    }

    public int GetDurability()
    {
        return _durability;
    }

    public void CaptureHit(Player capturer)
    {
        if (capturer == _player)
        {
            return;
        }
        int captureCost = GetCaptureHitCost(capturer);
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
        return capturer.ActionPoints.Amount >= GetCaptureHitCost(capturer);
    }

    public int GetCaptureHitCost(Player capturer)
    {
        return _captureHitCost;
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
