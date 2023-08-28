using castledice_game_logic.GameObjects;

namespace castledice_game_logic.MovesLogic;

public class MoveValidator
{
    private Board _board;
    private CellMovesSelector _cellMovesSelector;

    public MoveValidator(Board board)
    {
        _board = board;
        _cellMovesSelector = new CellMovesSelector(board);
    }

    public bool ValidateMove(AbstractMove move)
    {
        if (move.Position.X < 0 || move.Position.Y < 0)
        {
            return false;
        }
        if (!_board.HasCell(move.Position))
        {
            return false;
        }
        
        var cellMoves = _cellMovesSelector.SelectMoveCells(move.Player);
        if (move is PlaceMove)
        {
            var placeMove = move as PlaceMove;
            return ValidatePlaceMove(placeMove, cellMoves);
        }
        if (move is RemoveMove)
        {
            var removeMove = move as RemoveMove;
            return ValidateRemoveMove(removeMove, cellMoves);
        }
        if (move is UpgradeMove)
        {
            var upgradeMove = move as UpgradeMove;
            return ValidateUpgradeMove(upgradeMove, cellMoves);
        }

        if (move is CaptureMove)
        {
            var caputreMove = move as CaptureMove;
            return ValidateCaptureMove(caputreMove, cellMoves);
        }
        throw new NotImplementedException("No validation implemented for this type of move: " + move.GetType().Name);
    }

    private bool ValidatePlaceMove(PlaceMove move, List<CellMove> cellMoves)
    {
        if (cellMoves.Any(c => c.MoveType == MoveType.Place && c.Cell.Position == move.Position))
        {
            //TODO: Should there be checking for additional conditions? Like for bridge.
            return true;
        }
        return false;
    }

    private bool ValidateRemoveMove(RemoveMove move, List<CellMove> cellMoves)
    {
        if (cellMoves.Any(c => c.MoveType == MoveType.Remove && c.Cell.Position == move.Position))
        {
            //TODO: Should validator take player's a.p into account? 
            // And should it check player's turn?
            return true;
        }
        return false;
    }

    private bool ValidateUpgradeMove(UpgradeMove move, List<CellMove> cellMoves)
    {
        if (cellMoves.Any(c => c.MoveType == MoveType.Upgrade && c.Cell.Position == move.Position))
        {
            return true;
        }
        return false;
    }

    private bool ValidateCaptureMove(CaptureMove move, List<CellMove> cellMoves)
    {
        if (cellMoves.Any(c => c.MoveType == MoveType.Capture && c.Cell.Position == move.Position))
        {
            return true;
        }
        return false;
    }
}