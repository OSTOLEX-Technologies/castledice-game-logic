using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic.Rules;

namespace castledice_game_logic.MovesLogic;

public class PossibleMovesSelector
{
    private Board _board;
    private IPlaceablesFactory _placeablesFactory;
    private IPlacementListProvider _placementListProvider;

    public PossibleMovesSelector(Board board, IPlaceablesFactory placeablesFactory, IPlacementListProvider placementListProvider)
    {
        _board = board;
        _placeablesFactory = placeablesFactory;
        _placementListProvider = placementListProvider;
    }

    public List<AbstractMove> GetPossibleMoves(Player player, Vector2Int position)
    {
        if (!_board.HasCell(position))
        {
            return new List<AbstractMove>();
        }
        
        var cellMovesSelector = new CellMovesSelector(_board);
        var cellMoves = cellMovesSelector.SelectCellMoves(player);
        var cellMove = cellMoves.FirstOrDefault(c => c.Cell.Position == position);
        if (cellMove == null)
        {
            return new List<AbstractMove>();
        }

        if (cellMove.MoveType == MoveType.Upgrade)
        {
            return GetUpgradeMoves(player, cellMove.Cell);
        }
        if (cellMove.MoveType == MoveType.Capture)
        {
            return GetCaptureMoves(player, cellMove.Cell);
        }
        if (cellMove.MoveType == MoveType.Place)
        {
            return GetPlaceMoves(player, cellMove.Cell);
        }
        if (cellMove.MoveType == MoveType.Replace)
        {
            return GetReplaceMoves(player, cellMove.Cell);
        }
        throw new InvalidOperationException("Unfamiliar move type: " + cellMove.MoveType);
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
        if (captureCost <= player.ActionPoints.Amount)
        {
            var captureMove = new CaptureMove(player, cell.Position);
            moves.Add(captureMove);
        }

        return moves;
    }
    
    private List<AbstractMove> GetPlaceMoves(Player player, Cell cell)
    {
        var placementTypes = _placementListProvider.GetPlacementList(player);
        var moves = new List<AbstractMove>();
        foreach (var type in placementTypes)
        {
            var placeable = _placeablesFactory.CreatePlaceable(type, player);
            bool canPlace = placeable.CanBePlacedOn(cell);
            bool canAfford = player.ActionPoints.Amount >= placeable.GetPlacementCost();
            if (canPlace && canAfford)
            {
                var move = new PlaceMove(player, cell.Position, placeable);
                moves.Add(move);
            }
        }
        return moves;
    }

    private List<AbstractMove> GetReplaceMoves(Player player, Cell cell)
    {
        var placementTypes = _placementListProvider.GetPlacementList(player);
        var moves = new List<AbstractMove>();
        foreach (var type in placementTypes)
        {
            var placeable = _placeablesFactory.CreatePlaceable(type, player);
            int replaceCost = ReplaceRules.GetReplaceCost(_board, cell.Position, placeable);
            bool canPlace = placeable.CanBePlacedOn(cell);
            bool canAfford = player.ActionPoints.Amount >= replaceCost;
            if (canPlace && canAfford)
            {
                var move = new ReplaceMove(player, cell.Position, placeable);
                moves.Add(move);
            }
        }
        return moves;
    }
}