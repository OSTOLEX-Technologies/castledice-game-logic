﻿using castledice_game_logic.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace castledice_game_logic.MovesLogic.Snapshots;

[Serializable]
public abstract class AbstractMoveSnapshot : IActionSnapshot
{
    protected ActionType _actionType = ActionType.Move;
    protected int _playerId;
    protected Vector2Int _position;
    protected int _moveCost;

    [JsonConverter(typeof(StringEnumConverter))] 

    public ActionType ActionType => _actionType;
    
    public int PlayerId => _playerId;
    
    public Vector2Int Position => _position;
    
    [JsonConverter(typeof(StringEnumConverter))] 

    public abstract MoveType MoveType { get; }
    
    public  int MoveCost => _moveCost;

    protected AbstractMoveSnapshot(AbstractMove move, int moveCost)
    {
        _playerId = move.Player.Id;
        _position = move.Position;
        _moveCost = moveCost;
    }

    public abstract string GetJson();

    protected bool Equals(AbstractMoveSnapshot other)
    {
        return _actionType == other._actionType && _playerId == other._playerId && _position.Equals(other._position) && MoveType == other.MoveType && MoveCost == other.MoveCost;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AbstractMoveSnapshot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)_actionType, _playerId, _position, (int)MoveType, MoveCost);
    }
}