using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic.MovesLogic;

public class PossibleMovesSelector
{
    private readonly Board _board;
    private readonly IPlaceablesFactory _placeablesFactory;
    private readonly IDecksList _decksList;

    public PossibleMovesSelector(Board board, IPlaceablesFactory placeablesFactory, IDecksList decksList)
    {
        _board = board;
        _placeablesFactory = placeablesFactory;
        _decksList = decksList;
    }

    public List<AbstractMove> GetPossibleMoves(Player player, Vector2Int position)
    {
        if (!_board.HasCell(position))
        {
            return new List<AbstractMove>();
        }
        
        var cellMovesSelector = new CellMovesSelector(_board);
        var cell = _board[position];
        var cellMove = cellMovesSelector.GetCellMoveForCell(player, cell);

        return cellMove.MoveType switch
        {
            MoveType.None => new List<AbstractMove>(),
            MoveType.Upgrade => GetUpgradeMoves(player, cellMove.Cell),
            MoveType.Capture => GetCaptureMoves(player, cellMove.Cell),
            MoveType.Place => GetPlaceMoves(player, cellMove.Cell),
            MoveType.Replace => GetReplaceMoves(player, cellMove.Cell),
            MoveType.Remove => GetRemoveMoves(player, cellMove.Cell),
            _ => throw new InvalidOperationException("Unfamiliar move type: " + cellMove.MoveType)
        };
    }

    private List<AbstractMove> GetUpgradeMoves(Player player, Cell cell)
    {
        var upgradeMove = new UpgradeMove(player, cell.Position);
        return new List<AbstractMove>() { upgradeMove };
    }
    
    private List<AbstractMove> GetCaptureMoves(Player player, Cell cell)
    {
        var moves = new List<AbstractMove>();
        var captureCost = CaptureRules.GetCaptureCost(_board, cell.Position, player);
        if (captureCost > player.ActionPoints.Amount) return moves;
        var captureMove = new CaptureMove(player, cell.Position);
        moves.Add(captureMove);

        return moves;
    }
    
    private List<AbstractMove> GetPlaceMoves(Player player, Cell cell)
    {
        var placementTypes = _decksList.GetDeck(player.Id);
        var moves = new List<AbstractMove>();
        foreach (var type in placementTypes)
        {
            var placeable = _placeablesFactory.CreatePlaceable(type, player);
            bool canPlace = placeable.CanBePlacedOn(cell);
            bool canAfford = player.ActionPoints.Amount >= placeable.GetPlacementCost();
            if (!canPlace || !canAfford) continue;
            var move = new PlaceMove(player, cell.Position, placeable);
            moves.Add(move);
        }
        return moves;
    }

    private List<AbstractMove> GetReplaceMoves(Player player, Cell cell)
    {
        var placementTypes = _decksList.GetDeck(player.Id);
        var moves = new List<AbstractMove>();
        foreach (var type in placementTypes)
        {
            var placeable = _placeablesFactory.CreatePlaceable(type, player);
            int replaceCost = ReplaceRules.GetReplaceCost(_board, cell.Position, placeable);
            bool canPlace = placeable.CanBePlacedOn(cell);
            bool canAfford = player.ActionPoints.Amount >= replaceCost;
            if (!canPlace || !canAfford) continue;
            var move = new ReplaceMove(player, cell.Position, placeable);
            moves.Add(move);
        }
        return moves;
    }

    private List<AbstractMove> GetRemoveMoves(Player player, Cell cell)
    {
        var moves = new List<AbstractMove>();
        var removeCost = RemoveRules.GetRemoveCost(_board, cell.Position);
        if (removeCost > player.ActionPoints.Amount) return moves;
        var removeMove = new RemoveMove(player, cell.Position);
        moves.Add(removeMove);
        return moves;
    }
}