using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.Utilities;

namespace castledice_game_logic;

public class UnitBranchesCutter
{
    private Board _board;
    private bool[,] _connectedFields; //Connectivity matrix that represents which cells with units are connected to player base.
                                      //Should be set to false after each call of CutUnconnectedBranches method.

    public UnitBranchesCutter(Board board)
    {
        _board = board;
        _connectedFields = new bool[board.GetLength(0), board.GetLength(1)];
        ResetConnectedFields();
    }

    private void ResetConnectedFields()
    {
        for (int i = 0; i < _connectedFields.GetLength(0); i++)
        {
            for (int j = 0; j < _connectedFields.GetLength(1); j++)
            {
                _connectedFields[i, j] = false;
            }
        }
    }

    public void CutUnconnectedBranches(Player player)
    {
        ResetConnectedFields();
        List<Vector2Int> playerBasePositions = GetPlayerBasePositions(player);
        if (playerBasePositions.Count == 0)
        {
            RemoveAllUnitsOwnedByPlayer(player);
            FreeAllCapturablesOwnedByPlayer(player);
        }
        foreach (var position in playerBasePositions)
        {
            MarkConnectedCells(position.X, position.Y, player);
        }
        CutNotConnectedContent(player);
    }
    
    //Depth-first search to mark cells with units that are connected to player base. 
    private void MarkConnectedCells(int x, int y, Player player)
    {
        _connectedFields[x, y] = true;
        for (int i = - 1; i <= 1; i++)
        {
            for (int j = - 1; j <=  1; j++)
            {
                int newX = x + i;
                int newY = y + j;
                if (!_board.HasCell(newX, newY) || _connectedFields[newX, newY]) continue;
                var cell = _board[x, y];
                var newCell = _board[newX, newY];
                if (cell.HasContent(c => ContentChecker.ContentBelongsToPlayer(c, player)) &&
                    newCell.HasContent(c => ContentChecker.ContentBelongsToPlayer(c, player)))
                {
                    MarkConnectedCells(newX, newY, player);
                }
            }
        }
    }

    private void CutNotConnectedContent(Player player)
    {
        for (int i = 0; i < _connectedFields.GetLength(0); i++)
        {
            for (int j = 0; j < _connectedFields.GetLength(1); j++)
            {
                if (_board.HasCell(i, j) && !_connectedFields[i, j])
                {
                    CutContentOnCell(_board[i, j], player);
                }
            }
        }
    }

    private void CutContentOnCell(Cell cell, Player player)
    {
        var playerOwned = cell.GetContent().FirstOrDefault(c => ContentChecker.ContentBelongsToPlayer(c, player));
        if (playerOwned == null)
        {
            return;
        }
        if (playerOwned is IReplaceable replaceable)
        {
            cell.RemoveContent(playerOwned);
        }
        else if (playerOwned is ICapturable capturable && !ContentIsPlayerBase(playerOwned, player))
        {
            capturable.Free();
        }
    }

    
    private List<Vector2Int> GetPlayerBasePositions(Player player)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        foreach (var cell in _board)
        {
            if (cell.HasContent(content => ContentIsPlayerBase(content, player)))
            {
                positions.Add(cell.Position);
            }
        }
        return positions;
    }

    private void RemoveAllUnitsOwnedByPlayer(Player player)
    {
        foreach (var cell in _board)
        {
            var replaceable = cell.GetContent().FirstOrDefault(c => ContentChecker.ContentBelongsToPlayer(c, player) && c is IReplaceable);
            if (replaceable == null)
            {
                continue;
            }

            cell.RemoveContent(replaceable);
        }
    }

    private void FreeAllCapturablesOwnedByPlayer(Player player)
    {
        foreach (var cell in _board)
        {
            var capturable = cell.GetContent().FirstOrDefault(c => ContentChecker.ContentBelongsToPlayer(c, player) && 
                                                                   c is ICapturable &&
                                                                   !ContentIsPlayerBase(c, player)) as ICapturable;

            capturable?.Free();
        }
    }

    private bool ContentIsPlayerBase(Content content, Player player)
    {
        return content is Castle castle && castle.GetOwner() == player;
    }

}