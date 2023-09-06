namespace castledice_game_logic.GameObjects;

public interface ICapturable
{
    void Capture(Player capturer);

    bool CanBeCaptured(Player capturer);

    int GetCaptureCost(Player capturer);

    void Free();
}