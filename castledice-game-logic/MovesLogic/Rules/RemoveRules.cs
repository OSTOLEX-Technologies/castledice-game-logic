using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic.Rules;

public static class RemoveRules
{
    public static bool CanRemoveOnCell(Board board, Vector2Int position, Player player)
    {
        if (!CanRemoveOnCellIgnoreNeighbours(board, position, player))
        {
            return false;
        }

        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player);
    }

    public static bool CanRemoveOnCellIgnoreNeighbours(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            return false;
        }
        var cell = board[position];
        return cell.HasContent(c => ContentCanBeRemovedByPlayer(c, player));
    }
    
    private static bool ContentCanBeRemovedByPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var playerOwned = content as IPlayerOwned;
            if (playerOwned.GetOwner() == player)
            {
                return false;
            }
        }
        if (content is IRemovable)
        {
            var removable = content as IRemovable;
            if (removable.CanBeRemoved())
            {
                int removeCost = removable.GetMinimalRemoveCost();
                int playerActionPoints = player.ActionPoints.Amount;
                return removeCost <= playerActionPoints;
            }
        }
        return false;
    }
}