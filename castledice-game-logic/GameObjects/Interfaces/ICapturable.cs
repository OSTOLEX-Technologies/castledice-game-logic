namespace castledice_game_logic.GameObjects;

public interface ICapturable
{
    bool TryCapture(Player capturer);
}