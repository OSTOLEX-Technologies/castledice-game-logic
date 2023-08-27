using castledice_game_logic.GameObjects;

namespace castledice_game_logic.MovesLogic;

public class CellMovesSelector
{
    private Board _board;

    public CellMovesSelector(Board board)
    {
        _board = board;
    }

    public List<CellMove> SelectMoveCells(Player player)
    {
        List<CellMove> selectedCells = new List<CellMove>();
        foreach (var cell in _board)
        {
            if (CellContentOwnedByPlayer(cell, player) && CellContentIsUpgradeable(cell))
            {
                selectedCells.Add(new CellMove(cell, MoveType.Upgrade));
            }
            else if (HasNeighbourOwnedByPlayer(cell, player))
            {
                
                if (CellContentIsCapturable(cell))
                {
                    selectedCells.Add(new CellMove(cell, MoveType.Capture));

                }
                else if(CellContentIsRemovable(cell))
                { 
                    selectedCells.Add(new CellMove(cell, MoveType.Remove));
                }
                else if (CanPlaceOnCell(cell, player))
                {
                    selectedCells.Add(new CellMove(cell, MoveType.Place));
                }
            }
        }
        return selectedCells;
    }

    private bool CellContentOwnedByPlayer(Cell cell, Player player)
    {
        return cell.HasContent(c => ContentBelongsToPlayer(c, player));
    }


    private bool CellContentIsUpgradeable(Cell cell)
    {
        return cell.HasContent(ContentCanBeUpgraded);
    }

    private bool ContentCanBeUpgraded(Content content)
    {
        if (content is IUpgradeable)
        {
            var upgradeable = content as IUpgradeable;
            return upgradeable.CanBeUpgraded();
        }

        return false;
    }

    
    private bool HasNeighbourOwnedByPlayer(Cell cell, Player player)
    {
        var cellPosition = cell.Position;
        for (int i = cellPosition.X - 1; i <= cellPosition.X + 1; i++)
        {
            for (int j = cellPosition.Y - 1; j <= cellPosition.Y + 1; j++)
            {
                if (cellPosition.X == i && cellPosition.Y == j)
                {
                    continue;
                }
                if (_board.HasCell(i, j))
                {
                    var neighbour = _board[i, j];
                    if (neighbour.HasContent(c => ContentBelongsToPlayer(c, player)))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
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

    private bool CellContentIsCapturable(Cell cell)
    {
        return cell.HasContent(c => c is ICapturable);
    }

    private bool CellContentIsRemovable(Cell cell)
    {
        return cell.HasContent(ContentCanBeRemoved);
    }

    private bool ContentCanBeRemoved(Content content)
    {
        if (content is IRemovable)
        {
            var removable = content as IRemovable;
            return removable.CanBeRemoved();
        }

        return false;
    }
}