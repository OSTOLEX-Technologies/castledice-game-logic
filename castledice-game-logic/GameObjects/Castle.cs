﻿namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    private int _durability;
    private int _defaultDurability;
    
    public Castle(Player player, int durability)
    {
        if (durability <= 0)
        {
            throw new ArgumentException("Durability must be positive!");
        }
        _player = player;
        _durability = durability;
        _defaultDurability = durability;
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
            return;
        }

        
        capturer.ActionPoints.DecreaseActionPoints(_durability);
        _durability = 0;

        if (_durability <= 0)
        {
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
       
    }

    public Player GetOwner()
    {
        return _player;
    }

    public bool IsBlocking()
    {
        return true;
    }
}
