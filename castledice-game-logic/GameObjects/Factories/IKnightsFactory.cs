namespace castledice_game_logic.GameObjects.Factories;

public interface IKnightsFactory
{
    Knight GetKnight(Player owner);
}