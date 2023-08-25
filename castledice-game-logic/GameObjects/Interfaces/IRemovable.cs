namespace castledice_game_logic.GameObjects;

public interface IRemovable
{
    bool TryRemove(Player remover, int replacementCost);
}