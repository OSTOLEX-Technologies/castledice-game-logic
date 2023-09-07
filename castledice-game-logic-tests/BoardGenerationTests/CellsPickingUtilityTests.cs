using System.Collections;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;



public class CellsPickingUtilityTests
{
    //TODO: Ask about better naming for test data classes
    public class ExcludeCellsAroundTestCases : IEnumerable<object[]>
    {

        private readonly List<object[]> _data = new List<object[]>()
        {
            ObstacleInTheMiddleRadiusOneCase(),
            ObstacleInTheLeftUpperCornerRadiusTwoCase(),
            ObstacleInTheMiddleRadiusTwoCase(),
            ObstacleInTheMiddleRadiusThreeCase()
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] ObstacleInTheMiddleRadiusOneCase()
        {
            var board = GetFullNByNBoard(5);
            board[2, 2].AddContent(GetObstacle());
            var expectedMatrix = new bool[,]
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, false, true, false, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
            };
            int radius = 1;
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is IPlaceBlocking);
            return new object[] { board, expectedMatrix, predicate, radius };
        }

        private static object[] ObstacleInTheLeftUpperCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            board[0, 0].AddContent(GetObstacle());
            var expectedMatrix = new bool[,]
            {
                { true, false, false, true, true },
                { false, false, false, true, true },
                { false, false, true, true, true },
                { true, true, true, true, true },
                { true, true, true, true, true },
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is IPlaceBlocking);
            int radius = 2;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
        
        private static object[] ObstacleInTheMiddleRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            board[2, 2].AddContent(GetObstacle());
            var expectedMatrix = new bool[,]
            {
                { true, false, false, false, true },
                { false, false, false, false, false },
                { false, false, true, false, false },
                { false, false, false, false, false },
                { true, false, false, false, true },
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is IPlaceBlocking);
            int radius = 2;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
        
        private static object[] ObstacleInTheMiddleRadiusThreeCase()
        {
            var board = GetFullNByNBoard(7);
            board[3, 3].AddContent(GetObstacle());
            var expectedMatrix = new bool[,]
            {
                {true, true, false, false, false, true, true},
                {true, false, false, false, false, false, true},
                {false, false, false, false, false, false, false},
                {false, false, false, true, false, false, false},
                {false, false, false, false, false, false, false},
                {true, false, false, false, false, false, true},
                {true, true, false, false, false, true, true},
            };
            Func<Cell, bool> predicate = c => c.HasContent(ct => ct is IPlaceBlocking);
            int radius = 3;
            return new object[] { board, expectedMatrix, predicate, radius };
        }
    }
    
    public class CountIntersectionsForCellTestCases : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            UpperLeftCornerRadiusTwoCase(),
            UpperRightCornerRadiusTwoCase(),
            LowerRightCornerRadiusThreeCase(),
            LowerLeftCornerRadiusOneCase(),
            MiddleRadiusTwoCase(),
            CellNearToExcludedRowsAndColsRadiusOneCase()
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] UpperLeftCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            var cellPosition = new Vector2Int(0, 0);
            int radius = 2;
            int expectedIntersectionsCount = 13;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] UpperRightCornerRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            var cellPosition = new Vector2Int(0, 4);
            int radius = 2;
            int expectedIntersectionsCount = 13;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] LowerRightCornerRadiusThreeCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            var cellPosition = new Vector2Int(4, 4);
            int radius = 3;
            int expectedIntersectionsCount = 24;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
        
        private static object[] LowerLeftCornerRadiusOneCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            var cellPosition = new Vector2Int(4, 0);
            int radius = 1;
            int expectedIntersectionsCount = 5;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }

        private static object[] MiddleRadiusTwoCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            var cellPosition = new Vector2Int(2, 2);
            int radius = 2;
            int expectedIntersectionsCount = 0;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }

        private static object[] CellNearToExcludedRowsAndColsRadiusOneCase()
        {
            var board = GetFullNByNBoard(5);
            var picker = new CellsPickingUtility(board);
            picker.ExcludeColumns(2);
            picker.ExcludeRows(2);
            var cellPosition = new Vector2Int(3, 3);
            int radius = 1;
            int expectedIntersectionsCount = 5;
            return new object[] { picker, cellPosition, radius, expectedIntersectionsCount };
        }
    }

    //TODO: Ask if we can use entities such as AvailabilityMatrix (which do not really exist) in naming
    [Fact]
    public void AvailabilityMatrix_ShouldHaveFalseValues_ForNonExistingCells()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        var expectedMatrix = new bool[,]
        {
            { true, false },
            { false, true }
        };
        var picker = new CellsPickingUtility(board);

        var actualMatrix = picker.GetAvailabilityMatrix();

        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void GetAvailabilityMatrix_ShouldNotReturnReferenceToTheOriginalMatrix()
    {
        var board = new Board(CellType.Square);
        board.AddCell(1, 1);
        board.AddCell(0, 0);
        var expectedMatrix = new bool[,]
        {
            { true, false },
            { false, true }
        };
        var picker = new CellsPickingUtility(board);

        var matrixToModify = picker.GetAvailabilityMatrix();
        matrixToModify[0, 1] = true;
        var actualMatrix = picker.GetAvailabilityMatrix();
        
        Assert.NotEqual(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void ExcludeRows_ShouldSetAvailabilityMatrixRowsToFalse_ForGivenIndices()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPickingUtility(board);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { true, true, true },
            { true, true, true },
        };
        cellPicker.ExcludeRows(0);

        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void ExcludeColumns_ShouldSetAvailabilityMatrixColumnsToFalse_ForGivenIndices()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPickingUtility(board);
        var expectedMatrix = new bool[,]
        {
            { false, true, true },
            { false, true, true },
            { false, true, true },
        };
        cellPicker.ExcludeColumns(0);

        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }
    
    [Fact]
    public void ExcludeCells_ShouldSetAvailabilityMatrixCellsToFalse_ByPredicate()
    {
        var board = GetFullNByNBoard(4);
        board[0, 0].AddContent(GetObstacle());
        board[1, 2].AddContent(GetObstacle());
        board[2, 1].AddContent(GetObstacle());
        var expectedMatrix = new bool[,]
        {
            { false, true, true, true },
            { true, true, false, true },
            { true, false, true, true },
            { true, true, true, true },
        };
        var cellPicker = new CellsPickingUtility(board);

        cellPicker.ExcludeCells(c => c.HasContent() == true);
        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void ExcludeCellsAround_ShouldThrowArgumentException_IfNegativeRadiusGiven()
    {
        var board = new Board(CellType.Square);
        var cellPicker = new CellsPickingUtility(board);
        Func<Cell, bool> predicate = c => c.HasContent(ct => ct is Tree);
        Assert.Throws<ArgumentException>(() => cellPicker.ExcludeCellsAround(predicate, -1));
    }

    [Theory]
    [ClassData(typeof(ExcludeCellsAroundTestCases))]
    public void ExcludeCellsAround_SetsAvailabilityMatrixCellsToFalse_InGivenRadiusAroundAppropriateCell(Board board, 
        bool[,] expectedMatrix, 
        Func<Cell, bool> predicate,
        int radius)
    {
        var cellPicker = new CellsPickingUtility(board);
        
        cellPicker.ExcludeCellsAround(predicate, radius);
        var actualMatrix = cellPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void PickRandom_ShouldNotReturnCells_WhichAreNotAvailable()
    {
        var board = GetFullNByNBoard(3);
        var centralCell = board[1, 1];
        var cellsPicker = new CellsPickingUtility(board);
        cellsPicker.ExcludeRows(0, 2);
        cellsPicker.ExcludeColumns(0, 2);

        var actualCell = cellsPicker.PickRandom();
        
        Assert.Same(centralCell, actualCell);
    }

    //TODO: Ask if it is a good idea to throw an exception in such situation
    [Fact]
    public void PickRandom_ShouldThrowInvalidOperationException_IfNoCellsAvailable()
    {
        var board = GetFullNByNBoard(2);
        var cellsPicker = new CellsPickingUtility(board);
        cellsPicker.ExcludeColumns(0, 1);
        cellsPicker.ExcludeRows(0, 1);

        Assert.Throws<InvalidOperationException>(() => cellsPicker.PickRandom());
    }

    [Fact]
    public void ExcludePicked_ShouldThrowInvalidOperationException_IfCalledBeforePickingAnyCell()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPickingUtility(board);

        Assert.Throws<InvalidOperationException>(() => cellPicker.ExcludePicked());
    }

    [Fact]
    public void ExcludePicked_ShouldSetPickedCellToFalseInTheAvailabilityMatrix()
    {
        var board = GetFullNByNBoard(3);
        var cellsPicker = new CellsPickingUtility(board);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { false, false, false },
            { false, false, false },
        };
        cellsPicker.ExcludeColumns(0, 2);
        cellsPicker.ExcludeRows(0, 2);

        cellsPicker.PickRandom();
        cellsPicker.ExcludePicked();
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void ExcludeAroundPicked_ShouldThrowInvalidOperationException_IfCalledBeforePickingAnyCell()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPickingUtility(board);
        int radius = 1;

        Assert.Throws<InvalidOperationException>(() => cellPicker.ExcludeAroundPicked(radius));
    }

    [Fact]
    public void ExcludeAroundPicked_ShouldThrowArgumentException_IfNegativeRadiusGiven()
    {
        var board = GetFullNByNBoard(3);
        var cellPicker = new CellsPickingUtility(board);
        int radius = -1;
        cellPicker.PickRandom();

        Assert.Throws<ArgumentException>(() => cellPicker.ExcludeAroundPicked(radius));
    }

    [Fact]
    public void ExcludeAroundPicked_ShouldSetCorrespondingCellsInMatrixToFalse_InGivenRadius()
    {
        var board = GetFullNByNBoard(3);
        var randomGenerator = new Mock<IRangeRandomNumberGenerator>();
        randomGenerator.Setup(rnd => rnd.GetRandom(1, 10)).Returns(5);
        var cellsPicker = new CellsPickingUtility(board, randomGenerator.Object);
        var expectedMatrix = new bool[,]
        {
            { false, false, false },
            { false, true, false },
            { false, false, false }
        };

        cellsPicker.PickRandom();
        cellsPicker.ExcludeAroundPicked(1);
        var actualMatrix = cellsPicker.GetAvailabilityMatrix();
        
        Assert.Equal(expectedMatrix, actualMatrix);
    }

    //TODO: Another test with questionable name
    [Theory]
    [ClassData(typeof(CountIntersectionsForCellTestCases))]
    public void CountIntersectionsForCell_ShouldReturnValidIntersectionsNumber(CellsPickingUtility pickingUtility, Vector2Int cellPosition, 
        int radius, int expectedIntersectionsCount)
    {
        int actualIntersectionsCount = pickingUtility.CountIntersectionsForCell(cellPosition, radius);
        Assert.Equal(expectedIntersectionsCount, actualIntersectionsCount);
    }

    [Fact]
    public void CountIntersectionsForCell_ShouldThrowArgumentException_IfNegativeRadiusGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var cellPosition = new Vector2Int(0, 0);
        int radius = -1;

        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));
    }

    [Fact]
    public void CountIntersectionsForCell_ShouldThrowArgumentException_IfNegativeCellCoordinateGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var cellPosition = new Vector2Int(-1, -1);
        int radius = 1;

        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));
    }

    [Fact]
    public void CountIntersectionsForCell_ShouldThrowArgumentException_IfCellCoordinatesAreOutsideOfBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var cellPosition = new Vector2Int(3, 3);
        int radius = 1;
        
        Assert.Throws<ArgumentException>(() => picker.CountIntersectionsForCell(cellPosition, radius));

    }

    [Fact]
    public void ExcludeCell_ShouldSetFalseInAvailabilityMatrix_ForCellWithGivenIndex()
    {
        var board = GetFullNByNBoard(3);
        var expectedMatrix = new bool[,]
        {
            { true, true, true },
            { true, false, true },
            { true, true, true }
        };
        var picker = new CellsPickingUtility(board);
        var cellPosition = new Vector2Int(1, 1);
        
        picker.ExcludeCell(cellPosition);
        var actualMatrix = picker.GetAvailabilityMatrix();

        Assert.Equal(expectedMatrix, actualMatrix);
    }

    [Fact]
    public void ExcludeCell_ShouldThrowArgumentException_IfNegativePositionGiven()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var position = new Vector2Int(-1, -1);

        Assert.Throws<ArgumentException>(() => picker.ExcludeCell(position));
    }

    [Fact]
    public void ExcludeCell_ShouldThrowArgumentException_IfCellPositionIsOutsideOfBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var position = new Vector2Int(4, 4);

        Assert.Throws<ArgumentException>(() => picker.ExcludeCell(position));
    }

    [Fact]
    public void IncludeCell_ShouldThrowArgumentException_IfCellPositionIsNegative()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var position = new Vector2Int(-1, -1);

        Assert.Throws<ArgumentException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void IncludeCell_ShouldThrowArgumentException_IfCellPositionIsOutsideOfBoard()
    {
        var board = GetFullNByNBoard(3);
        var picker = new CellsPickingUtility(board);
        var position = new Vector2Int(4, 4);

        Assert.Throws<ArgumentException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void IncludeCell_ShouldThrowInvalidOperationException_IfCellDoesntExistOnBoard()
    {
        var board = new Board(CellType.Square);
        board.AddCell(2, 2);
        var picker = new CellsPickingUtility(board);
        var position = new Vector2Int(0, 0);

        Assert.Throws<InvalidOperationException>(() => picker.IncludeCell(position));
    }

    [Fact]
    public void PickSmart_ShouldThrowInvalidOperationException_IfNoCellsAvailable()
    {
        var board = GetFullNByNBoard(2);
        var picker = new CellsPickingUtility(board);
        picker.ExcludeColumns(0, 1);
        picker.ExcludeRows(0, 1);

        Assert.Throws<InvalidOperationException>(() => picker.PickSmart(2));
    }
    
    //TODO: Another strange moment to consider
    [Fact]
    public void PickSmart_ShouldReturnCell_ThatTakesLeastSpaceIfExcludeAroundPickedCalledAfter()
    {
        var board = GetFullNByNBoard(5);
        var cellsPicker = new CellsPickingUtility(board);
        cellsPicker.ExcludeColumns(0, 1);
        cellsPicker.ExcludeRows(0, 1);
        cellsPicker.IncludeCell(new Vector2Int(0, 0));
        var expectedCell = board[0, 0];

        var actualCell = cellsPicker.PickSmart(2);
        
        Assert.Equal(expectedCell, actualCell);
    }
}