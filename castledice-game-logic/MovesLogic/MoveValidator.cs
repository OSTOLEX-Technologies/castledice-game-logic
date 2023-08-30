﻿using castledice_game_logic.GameObjects;
using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic.MovesLogic;


//TODO: Moves validator should take into account the current player turn
public class MoveValidator : IMoveVisitor
{
    private Board _board;
    private CellMovesSelector _cellMovesSelector;
    private PlayerTurnsSwitcher _turns;

    public MoveValidator(Board board, PlayerTurnsSwitcher turns)
    {
        _board = board;
        _cellMovesSelector = new CellMovesSelector(board);
        _turns = turns;
    }

    public bool ValidateMove(AbstractMove move)
    {
        if (!_board.HasCell(move.Position))
        {
            return false;
        }
        if (move.Player != _turns.GetCurrent())
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
        if (!cellMoves.Any(c => c.MoveType == MoveType.Place && c.Cell.Position == move.Position)) return false;
        var placeable = move.ContentToPlace;
        var cell = _board[move.Position];
        var placeCost = placeable.GetPlacementCost();
        var playerActionPoints = move.Player.ActionPoints.Amount;
        return placeCost <= playerActionPoints && placeable.CanBePlacedOn(cell);
    }

    private bool ValidateRemoveMove(RemoveMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        if (!cellMoves.Any(c => c.MoveType == MoveType.Remove && c.Cell.Position == move.Position)) return false;
        var cell = _board[move.Position];
        var removable = cell.GetContent().FirstOrDefault(c => c is IRemovable) as IRemovable;
        var removeCost = removable.GetRemoveCost(move.Replacement.GetPlacementCost());
        var playerActionPoints = move.Player.ActionPoints.Amount;
        return removeCost <= playerActionPoints;
    }

    private bool ValidateUpgradeMove(UpgradeMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        return cellMoves.Any(c => c.MoveType == MoveType.Upgrade && c.Cell.Position == move.Position);
    }

    private bool ValidateCaptureMove(CaptureMove move)
    {
        var cellMoves = _cellMovesSelector.SelectCellMoves(move.Player);
        return cellMoves.Any(c => c.MoveType == MoveType.Capture && c.Cell.Position == move.Position);
    }
}