namespace castledice_game_logic.MovesLogic;


//TODO: Moves validator should take into account the current player turn
public class MoveValidator : IMoveVisitor
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
        if (!_board.HasCell(move.Position))
        {
            return false;
        }
        return move.Accept(this);
    }
    
    public bool VisitPlaceMove(PlaceMove move)
    {
        return ValidatePlaceMove(move);
    }

    public bool VisitRemoveMove(RemoveMove move)
    {
        return ValidateRemoveMove(move);
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        return ValidateUpgradeMove(move);
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        return ValidateCaptureMove(move);
    }

    private bool ValidatePlaceMove(PlaceMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        if (cellMoves.Any(c => c.MoveType == MoveType.Place && c.Cell.Position == move.Position))
        {
            //TODO: Should there be checking for additional conditions? Like for bridge.
            return true;
        }
        return false;
    }

    private bool ValidateRemoveMove(RemoveMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        if (cellMoves.Any(c => c.MoveType == MoveType.Remove && c.Cell.Position == move.Position))
        {
            //TODO: Should validator take player's a.p into account? 
            // And should it check player's turn?
            return true;
        }
        return false;
    }

    private bool ValidateUpgradeMove(UpgradeMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        if (cellMoves.Any(c => c.MoveType == MoveType.Upgrade && c.Cell.Position == move.Position))
        {
            return true;
        }
        return false;
    }

    private bool ValidateCaptureMove(CaptureMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        if (cellMoves.Any(c => c.MoveType == MoveType.Capture && c.Cell.Position == move.Position))
        {
            return true;
        }
        return false;
    }


}