namespace castledice_game_logic.GameObjects;

public interface ICapturable
{
    void CaptureHit(Player capturer);

    bool CanBeCaptured(Player capturer);

    int GetCaptureHitCost(Player capturer);

    void Free();
    /// <summary>
    /// This methods returns how many hits are left to capture this object.
    /// </summary>
    /// <param name="capturer"></param>
    /// <returns></returns>
    int CaptureHitsLeft(Player capturer);
}