namespace castledice_game_logic.GameObjects;

public class Castle : GameObject
{
    private Player _player;

    public Player Player => _player;

    public Castle(Player player)
    {
        _player = player;
    }
}