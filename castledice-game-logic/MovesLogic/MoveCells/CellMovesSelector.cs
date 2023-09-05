using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic.MovesLogic;

public class CellMovesSelector
{
    private Board _board;

    public CellMovesSelector(Board board)
    {
        _board = board;
    }

    public List<CellMove> SelectCellMoves(Player player)
    {
        List<CellMove> selectedCells = new List<CellMove>();
        foreach (var cell in _board)
        {
            var cellMove = GetCellMoveForCell(player, cell);
            if (cellMove.MoveType != MoveType.None)
            {
                selectedCells.Add(cellMove);
            }
        }
        return selectedCells;
    }

    public CellMove GetCellMoveForCell(Player player, Cell cell)
    {
        if (CellContentOwnedByPlayer(cell, player))
        {
            if (CanUpgradeOnCell(cell, player))
            {
                return new CellMove(cell, MoveType.Upgrade);
            }
        }
        else if (HasNeighbourOwnedByPlayer(cell, player))
        {
            if (CanCaptureOnCell(cell, player))
            {
                return new CellMove(cell, MoveType.Capture);
            }
            if(CanRemoveOnCell(cell, player))
            { 
                return new CellMove(cell, MoveType.Replace);
            }
            if (CanPlaceOnCell(cell, player))
            {
                return new CellMove(cell, MoveType.Place);
            }
        }
        return new CellMove(cell, MoveType.None);
    }

    private bool CellContentOwnedByPlayer(Cell cell, Player player)
    {
        return cell.HasContent(c => ContentBelongsToPlayer(c, player));
    }
    
    private bool ContentBelongsToPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var ownedContent = content as IPlayerOwned;
            var owner = ownedContent.GetOwner();
            return player == owner;
        }
        return false;
    }

    private bool CanUpgradeOnCell(Cell cell, Player player)
    {
        return UpgradeRules.CanUpgradeOnCell(cell, player);
    }
    
    private bool HasNeighbourOwnedByPlayer(Cell cell, Player player)
    {
        return CellNeighboursChecker.HasNeighbourOwnedByPlayer(_board, cell.Position, player);
    }
    
    private bool CanPlaceOnCell(Cell cell, Player player)
    {
        return !cell.HasContent(c => ContentBelongsToOtherPlayer(c, player) || ContentIsObstacle(c));
    }
    
    private bool ContentBelongsToOtherPlayer(Content content, Player player)
    {
        if (content is IPlayerOwned)
        {
            var ownedContent = content as IPlayerOwned;
            var owner = ownedContent.GetOwner();
            return player != owner;
        }
        return false;
    }

    private bool ContentIsObstacle(Content content)
    {
        return content is Tree;
    }

    private bool CanCaptureOnCell(Cell cell, Player player)
    {
        return CaptureRules.CanCaptureOnCellIgnoreNeighbours(_board, cell.Position, player);
    }

    private bool CanRemoveOnCell(Cell cell, Player player)
    {
        return ReplaceRules.CanReplaceOnCellIgnoreNeighbours(_board, cell.Position, player);
    }
}