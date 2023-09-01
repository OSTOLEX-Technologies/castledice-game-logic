using castledice_game_logic.GameObjects;

namespace castledice_game_logic.MovesLogic;

//TODO: Ask if it is a good idea to make sub applier classes like UpgradeMoveApplier or PlaceMoveApplier to not validate SRP principle.
public class MoveApplier : IMoveVisitor
{
    private Board _board;
    
    public void ApplyMove(AbstractMove move)
    {
        if (!_board.HasCell(move.Position))
        {
            throw new ArgumentException("Cannot apply move! Invalid move position: " + move.Position);
        }

        move.Accept(this);
    }

    public MoveApplier(Board board)
    {
        _board = board;
    }

    public bool VisitPlaceMove(PlaceMove move)
    {
        ApplyPlaceMove(move);
        return false;
    }

    private void ApplyPlaceMove(PlaceMove move)
    {
        var player = move.Player;
        var placeable = move.ContentToPlace;
        var position = move.Position;
        var cell = _board[position];
        if (placeable is not Content content)
        {
            throw new ArgumentException("Cannot apply place move! ContentToPlace type is not Content!");
        }
        player.ActionPoints.DecreaseActionPoints(placeable.GetPlacementCost());
        cell.AddContent(content);
    }

    public bool VisitReplaceMove(ReplaceMove move)
    {
        ApplyReplaceMove(move);
        return false;
    }

    private void ApplyReplaceMove(ReplaceMove move)
    {
        var player = move.Player;
        var replacement = move.Replacement;
        var position = move.Position;
        var cell = _board[position];
        if (replacement is not Content newContent)
        {
            throw new ArgumentException("Cannot apply replace move! Replacement type is not Content!");
        }

        var replaceable = cell.GetContent().FirstOrDefault(c => c is IReplaceable) as IReplaceable;
        if (replaceable == null)
        {
            throw new ArgumentException("Cannot apply replace move! Cell has no IReplaceable objects!");
        }

        int replaceCost = replaceable.GetReplaceCost(replacement.GetPlacementCost());
        player.ActionPoints.DecreaseActionPoints(replaceCost);

        cell.RemoveContent(replaceable as Content);
        cell.AddContent(newContent);
    }

    public bool VisitUpgradeMove(UpgradeMove move)
    {
        ApplyUpgradeMove(move);
        return false;
    }

    private void ApplyUpgradeMove(UpgradeMove move)
    {
        var player = move.Player;
        var position = move.Position;
        var cell = _board[position];
        var upgradeable = cell.GetContent().FirstOrDefault(c => c is IUpgradeable) as IUpgradeable;
        if (upgradeable == null)
        {
            throw new ArgumentException("Cannot apply upgrade move! Cell has no IUpgradeable objects!");
        }

        int upgradeCost = upgradeable.GetUpgradeCost();
        player.ActionPoints.DecreaseActionPoints(upgradeCost);
        upgradeable.Upgrade();
    }

    public bool VisitCaptureMove(CaptureMove move)
    {
        ApplyCaptureMove(move);
        return false;
    }

    private void ApplyCaptureMove(CaptureMove move)
    {
        var player = move.Player;
        var cell = _board[move.Position];
        var capturable = cell.GetContent().FirstOrDefault(c => c is ICapturable) as ICapturable;
        if (capturable == null)
        {
            throw new ArgumentException("Cannot apply capture move! Cell has no ICapturable objects!");
        }
        capturable.Capture(player);
    }
}