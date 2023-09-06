using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic.Rules;

public static class ReplaceRules
{
    public static bool CanReplaceOnCell(Board board, Vector2Int position, Player player)
    {
        if (!CanReplaceOnCellIgnoreNeighbours(board, position, player))
        {
            return false;
        }

        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(board, position, player);
    }

    public static bool CanReplaceOnCellIgnoreNeighbours(Board board, Vector2Int position, Player player)
    {
        if (!board.HasCell(position))
        {
            return false;
        }
        var cell = board[position];
        return cell.HasContent(c => ContentCanBeReplacedByPlayer(c, player));
    }
    
    private static bool ContentCanBeReplacedByPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var playerOwned = content as IPlayerOwned;
            if (playerOwned.GetOwner() == player)
            {
                return false;
            }
        }
        if (content is IReplaceable)
        {
            var replaceable = content as IReplaceable;
            if (replaceable.CanBeReplaced())
            {
                int replaceCost = replaceable.GetReplaceCost();
                int playerActionPoints = player.ActionPoints.Amount;
                return replaceCost <= playerActionPoints;
            }
        }
        return false;
    }

    
    //TODO: Should GetReplaceCost also check if it is possible to place replacement on cell?
    public static int GetReplaceCost(Board board, Vector2Int position, IPlaceable replacement)
    {
        if (!board.HasCell(position))
        {
            throw new ArgumentException("No cell on position: " + position);
        }

        var replaceable = GetReplaceableOnPosition(board, position);


        return replaceable.GetReplaceCost() + replacement.GetPlacementCost() - 1;
    }
    
    private static IReplaceable GetReplaceableOnPosition(Board board, Vector2Int position)
    {
        var cell = board[position];
        var replaceable = cell.GetContent().FirstOrDefault(c => c is IReplaceable) as IReplaceable;
        if (replaceable == null)
        {
            throw new ArgumentException("No replaceable objects on position: " + position);
        }

        return replaceable;
    }
}