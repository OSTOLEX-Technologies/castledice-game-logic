namespace castledice_game_logic;

public class PlayerKicker
{
    private Board _board;

    public PlayerKicker(Board board)
    {
        _board = board;
    }

    /// <summary>
    /// When player is kicked from board all his castles are freed and all his units are removed.
    /// However, objects like traps and bombs, that is objects which are not units are not removed.
    /// </summary>
    /// <param name="player"></param>
    public void KickFromBoard(Player player)
    {
        
    }
}