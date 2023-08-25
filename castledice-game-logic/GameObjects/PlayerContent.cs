namespace castledice_game_logic.GameObjects;

public class PlayerContent : Content
{
    private Player _player;

    public Player Player => _player;

    public PlayerContent(Player player)
    {
        _player = player;
    }
}