namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    private int _durability;
    private readonly int _captureHitCost;
    private readonly int _maxDurability;
    private readonly int _maxFreeDurability; //Durability of the castle that has no owner.

    public event EventHandler<int>? Hit; 

    /// <summary>
    /// Parameters durability and freeDurability must be positive. Otherwise exception will be thrown.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="durability"></param>
    /// <param name="maxDurability"></param>
    /// <param name="maxFreeDurability"></param>
    /// <param name="captureHitCost"></param>
    /// <exception cref="ArgumentException"></exception>
    public Castle(Player player, int durability, int maxDurability, int maxFreeDurability, int captureHitCost)
    {
        if (durability <= 0)
        {
            throw new ArgumentException("Durability must be positive!");
        }

        if (maxFreeDurability <= 0)
        {
            throw new ArgumentException("Free durability must be positive!");
        }

        if (captureHitCost <= 0)
        {
            throw new ArgumentException("CaptureHit cost must be positive!");
        }

        if (maxDurability <= 0)
        {
            throw new ArgumentException("Max durability must be positive!");
        }

        if (durability > maxDurability && !player.IsNull)
        {
            throw new ArgumentException("Durability of owned castle must be less or equal to castle`s max durability!");
        }

        if (durability > maxFreeDurability && player.IsNull)
        {
            throw new ArgumentException("Durability of free castle must be less or equal to castle`s max free durability!");
        }

        _player = player;
        _durability = durability;
        _maxDurability = maxDurability;
        _maxFreeDurability = maxFreeDurability;
        _captureHitCost = captureHitCost;
    }

    /// <summary>
    /// Returns max durability if castle is occupied.
    /// Returns max free durability if castle is free.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentMaxDurability()
    {
        if (_player.IsNull)
        {
            return _maxFreeDurability;
        }

        return _maxDurability;
    }

    public int GetMaxDurability()
    {
        return _maxDurability;
    }
    
    public int GetMaxFreeDurability()
    {
        return _maxFreeDurability;
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
        OnStateModified();
        Hit?.Invoke(this, captureCost);
        if (_durability > 0) return;
        _player = capturer;
        _durability = _maxDurability;
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
        return GetCaptureHitCost();
    }

    public int GetCaptureHitCost()
    {
        return _captureHitCost;
    }

    public void Free()
    {
        _player = new NullPlayer();
        _durability = _maxFreeDurability;
        OnStateModified();
    }

    public int CaptureHitsLeft(Player capturer)
    {
        return _durability;
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
