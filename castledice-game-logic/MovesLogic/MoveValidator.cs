using castledice_game_logic.MovesLogic.Rules;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic.MovesLogic;

public class MoveValidator : IMoveVisitor<bool>
{
    private readonly Board _board;
    private readonly CellMovesSelector _cellMovesSelector;
    private readonly ICurrentPlayerProvider _currentPlayerProvider;

    public MoveValidator(Board board, ICurrentPlayerProvider currentPlayerProvider)
    {
        _board = board;
        _cellMovesSelector = new CellMovesSelector(board);
        _currentPlayerProvider = currentPlayerProvider;
    }

    public bool ValidateMove(AbstractMove move)
    {
        if (!_board.HasCell(move.Position))
        {
            return false;
        }
        if (move.Player != _currentPlayerProvider.GetCurrentPlayer())
        {
            return false;
        }
        return move.Accept(this);
    }
    
    public bool VisitPlaceMove(PlaceMove move)
    {
        return ValidatePlaceMove(move);
    }

    public bool VisitReplaceMove(ReplaceMove move)
    {
        return ValidateReplaceMove(move);
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        return ValidateUpgradeMove(move);
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        return ValidateCaptureMove(move);
    }

    public bool VisitRemoveMove(RemoveMove move)
    {
        return ValidateRemoveMove(move);
    }

    private bool ValidatePlaceMove(PlaceMove move)
    {
        var cell = _board[move.Position];
        var cellMove = _cellMovesSelector.GetCellMoveForCell(move.Player, cell);
        if (cellMove.MoveType != MoveType.Place) return false;
        var placeable = move.ContentToPlace;
        var placeCost = placeable.GetPlacementCost();
        var playerActionPoints = move.Player.ActionPoints.Amount;
        return placeCost <= playerActionPoints && placeable.CanBePlacedOn(cell);
    }

    private bool ValidateReplaceMove(ReplaceMove move)
    {
        var cell = _board[move.Position];
        var cellMove = _cellMovesSelector.GetCellMoveForCell(move.Player, cell);
        if (cellMove.MoveType != MoveType.Replace) return false;
        var replaceCost = ReplaceRules.GetReplaceCost(_board, move.Position, move.Replacement);
        var playerActionPoints = move.Player.ActionPoints.Amount;
        return replaceCost <= playerActionPoints;
    }

    private bool ValidateUpgradeMove(UpgradeMove move)
    {
        var cell = _board[move.Position];
        var cellMove = _cellMovesSelector.GetCellMoveForCell(move.Player, cell);
        return cellMove.MoveType == MoveType.Upgrade;
    }

    private bool ValidateCaptureMove(CaptureMove move)
    {
        var cell = _board[move.Position];
        var cellMove = _cellMovesSelector.GetCellMoveForCell(move.Player, cell);
        return cellMove.MoveType == MoveType.Capture;
    }

    private bool ValidateRemoveMove(RemoveMove move)
    {
        var cell = _board[move.Position];
        var cellMove = _cellMovesSelector.GetCellMoveForCell(move.Player, cell);
        return cellMove.MoveType == MoveType.Remove;
    }
}