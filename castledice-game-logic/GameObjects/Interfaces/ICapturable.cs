namespace castledice_game_logic.GameObjects;

public interface ICapturable
{
    void CaptureHit(Player capturer);

    bool CanBeCaptured(Player capturer);

    int GetCaptureHitCost(Player capturer);

    void Free();
}