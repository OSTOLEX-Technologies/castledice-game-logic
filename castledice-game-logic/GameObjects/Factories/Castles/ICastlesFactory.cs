namespace castledice_game_logic.GameObjects.Factories;

public interface ICastlesFactory
{
    Castle GetCastle(Player owner);
}