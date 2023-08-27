using castledice_game_logic.GameObjects;

namespace castledice_game_logic.MovesLogic;

public class MoveCellsSelector
{
    private Board _board;

    public MoveCellsSelector(Board board)
    {
        _board = board;
    }

    public List<MoveCell> SelectMoveCells(Player player)
    {
        List<MoveCell> selectedCells = new List<MoveCell>();
        foreach (var cell in _board)
        {
            if (CellContentOwnedByPlayer(cell, player) && CellContentIsUpgradeable(cell))
            {
                selectedCells.Add(new MoveCell(cell, MoveType.Upgrade));
            }
            else if (AtLeastOneCellNeighbourContentOwnedByPlayer(cell, player))
            {
                
                if (CellContentIsCapturable(cell))
                {
                    selectedCells.Add(new MoveCell(cell, MoveType.Capture));

                }
                else if(CellContentIsRemovable(cell))
                { 
                    selectedCells.Add(new MoveCell(cell, MoveType.Remove));
                }
                else if (CanPlaceOnCell(cell, player))
                {
                    selectedCells.Add(new MoveCell(cell, MoveType.Place));
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
        return cell.HasContent(c => c is IUpgradeable);
    }

    
    //TODO: Ask what to do if we have different cell form?
    private bool AtLeastOneCellNeighbourContentOwnedByPlayer(Cell cell, Player player)
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
            if (player == owner)
            {
                return true;
            }
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
            if (player != owner)
            {
                return true;
            }
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
        return cell.HasContent(c => c is IRemovable);
    }
}