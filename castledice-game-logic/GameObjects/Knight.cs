namespace castledice_game_logic.GameObjects;

public class Knight : PlayerContent
{
    public override bool IsRemovable {
        get
        {
            return true;
        } 
    }

    public Knight(Player player) : base(player)
    {
    }
}