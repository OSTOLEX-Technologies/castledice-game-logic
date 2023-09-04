using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

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

    public static int GetUpgradeCost(Board board, Vector2Int position)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }
        var upgradeable = GetUpgradableOnPosition(board, position);
        return upgradeable.GetUpgradeCost();
    }
    
    private static IUpgradeable GetUpgradableOnPosition(Board board, Vector2Int position)
    {
        var cell = board[position];
        var upgradeable = cell.GetContent().FirstOrDefault(c => c is IUpgradeable) as IUpgradeable;
        if (upgradeable == null)
        {
            throw new ArgumentException("No upgradeable on position: " + position);
        }
        return upgradeable;
    }
}