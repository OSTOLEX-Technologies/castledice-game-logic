using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;

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
        if (content is not IRemovable removable) return false;
        bool canRemove = removable.CanBeRemoved();
        bool canAfford = removable.GetRemoveCost() <= player.ActionPoints.Amount;
        return canAfford && canRemove;
    }
    
    public static int GetRemoveCost(Board board, Vector2Int position)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }

        var removable = GetRemovableContentOnPosition(board, position);
        return removable.GetRemoveCost();
    }

    private static IRemovable GetRemovableContentOnPosition(Board board, Vector2Int position)
    {
        var cell = board[position];
        var removable = cell.GetContent().Find(c => c is IRemovable) as IRemovable;
        if (removable == null)
        {
            throw new ArgumentException("No replaceable objects on position: " + position);
        }
        return removable;
    }
}