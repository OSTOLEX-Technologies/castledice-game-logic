using castledice_game_logic.GameObjects;

namespace castledice_game_logic.MovesLogic.Rules;

/// <summary>
/// This class contains methods for checking possibility of upgrades on certain cells.
/// </summary>
public static class UpgradeRules
{
    public static bool CanUpgradeOnCell(Cell cell, Player player)
    {
        return cell.HasContent(c => ContentCanBeUpgradedByPlayer(c, player));
    }
    
    private static bool ContentCanBeUpgradedByPlayer(Content content, Player player)
    {
        if (content is IUpgradeable)
        {
            var upgradeable = content as IUpgradeable;
            if (upgradeable.CanBeUpgraded())
            {
                int upgradeCost = upgradeable.GetUpgradeCost();
                return upgradeCost <= player.ActionPoints.Amount;
            }
        }

        return false;
    }
}