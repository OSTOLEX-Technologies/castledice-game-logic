using System.Diagnostics;
using castledice_game_logic;

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
            else if (CellNeighboursContentOwnedByPlayer(cell, player))
            {
                if (CanPlaceOnCell(cell))
                {
                    selectedCells.Add(new MoveCell(cell, MoveType.Place));
                }
                else if (CellContentIsCapturable(cell))
                {
                    selectedCells.Add(new MoveCell(cell, MoveType.Capture));

                }
                else if(CellContentIsRemovable(cell))
                { 
                    selectedCells.Add(new MoveCell(cell, MoveType.Remove));
                }
            }
        }
        return selectedCells;
    }

    private bool CellContentOwnedByPlayer(Cell cell, Player player)
    {
        return false;
    }

    private bool CellContentIsUpgradeable(Cell cell)
    {
        return false;
    }

    private bool CellNeighboursContentOwnedByPlayer(Cell cell, Player player)
    {
        return false;
    }

    private bool CanPlaceOnCell(Cell cell)
    {
        return false;
    }

    private bool CellContentIsCapturable(Cell cell)
    {
        return false;
    }

    private bool CellContentIsRemovable(Cell cell)
    {
        return false;
    }
}