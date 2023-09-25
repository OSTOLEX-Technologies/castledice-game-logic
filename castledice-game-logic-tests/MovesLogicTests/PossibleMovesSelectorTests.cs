using System.Collections;
using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class PossibleMovesSelectorTests
{
    private class GetPossiblePlaceMovesTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return CanPlaceOnlyKnightsCase();
            yield return CannotPlaceBridgeOnCellCase();
            yield return CanPlaceEverythingCase();
            yield return ExpensiveHeavyKnightCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] CanPlaceOnlyKnightsCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock() { ListToReturn = new List<PlacementType>() { PlacementType.Knight } };
            var knight = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Knight };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight }
                } 
            };
            var expectedList = new List<AbstractMove>()
            {
                new PlaceMove(player, position, knight)
            };
            return new object[]{player, listProvider, factory, position, expectedList};
        }

        private static object[] CannotPlaceBridgeOnCellCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                    PlacementType.Bridge
                }
            };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight};
            var bridge = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Bridge, CanPlaceOn = false };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                    { PlacementType.Bridge, bridge },
                } 
            }; 
            var expectedList = new List<AbstractMove>()
            {
                new PlaceMove(player, position, knight),
                new PlaceMove(player, position, heavyKnight)
            };
            return new object[]{player, listProvider, factory, position, expectedList};
        }

        private static object[] CanPlaceEverythingCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                    PlacementType.Bridge
                }
            };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight};
            var bridge = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Bridge };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                    { PlacementType.Bridge, bridge },
                } 
            }; 
            var expectedList = new List<AbstractMove>()
            {
                new PlaceMove(player, position, knight),
                new PlaceMove(player, position, heavyKnight),
                new PlaceMove(player, position, bridge)
            };
            return new object[]{player, listProvider, factory, position, expectedList};
        }

        private static object[] ExpensiveHeavyKnightCase()
        {
            var player = GetPlayer(actionPoints: 2);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                }
            };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight, Cost = 1};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight, Cost = 3};
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                } 
            }; 
            var expectedList = new List<AbstractMove>()
            {
                new PlaceMove(player, position, knight)
            };
            return new object[]{player, listProvider, factory, position, expectedList};
        }
    }
    
    private class GetPossibleReplaceMovesTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return CanReplaceOnlyWithKnightCase();
            yield return CannotReplaceWithBridgeOCase();
            yield return CanReplaceWithEverythingCase();
            yield return ReplaceIsTooExpensiveCase();
            yield return ExpensiveHeavyKnightCase();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static object[] CanReplaceOnlyWithKnightCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock() { ListToReturn = new List<PlacementType>() { PlacementType.Knight } };
            var knight = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Knight };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight }
                } 
            };
            var enemyReplaceable = new ReplaceableMock() { ReplaceCost = 1 };
            var expectedList = new List<AbstractMove>()
            {
                new ReplaceMove(player, position, knight)
            };
            return new object[]{player, enemyReplaceable, listProvider, factory, position, expectedList};
        }

        private static object[] CannotReplaceWithBridgeOCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                    PlacementType.Bridge
                }
            };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight};
            var bridge = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Bridge, CanPlaceOn = false };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                    { PlacementType.Bridge, bridge },
                } 
            };
            var enemyReplaceable = new ReplaceableMock(){ReplaceCost = 1};
            var expectedList = new List<AbstractMove>()
            {
                new ReplaceMove(player, position, knight),
                new ReplaceMove(player, position, heavyKnight)
            };
            return new object[]{player, enemyReplaceable, listProvider, factory, position, expectedList};
        }

        private static object[] ReplaceIsTooExpensiveCase()
        {
            var player = GetPlayer(actionPoints: 2);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                    PlacementType.Bridge
                }
            };
            var enemyReplaceable = new ReplaceableMock() {ReplaceCost = 5};
            var factory = new PlaceableMocksFactory();
            var expectedList = new List<AbstractMove>();
            return new object[]{player, enemyReplaceable, listProvider, factory, position, expectedList};
        }

        private static object[] CanReplaceWithEverythingCase()
        {
            var player = GetPlayer(actionPoints: 6);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                    PlacementType.Bridge
                }
            };
            var enemyReplaceable = new ReplaceableMock() { ReplaceCost = 1 };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight};
            var bridge = new PlaceableMock() { PlacementTypeToReturn = PlacementType.Bridge };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                    { PlacementType.Bridge, bridge },
                } 
            }; 
            var expectedList = new List<AbstractMove>()
            {
                new ReplaceMove(player, position, knight),
                new ReplaceMove(player, position, heavyKnight),
                new ReplaceMove(player, position, bridge)
            };
            return new object[]{player, enemyReplaceable, listProvider, factory, position, expectedList};
        }

        private static object[] ExpensiveHeavyKnightCase()
        {
            var player = GetPlayer(actionPoints: 4);
            var position = new Vector2Int(1, 1);
            var listProvider = new PlacementListProviderMock()
            {
                ListToReturn = new List<PlacementType>()
                {
                    PlacementType.Knight,
                    PlacementType.HeavyKnight,
                }
            };
            var knight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.Knight, Cost = 1};
            var heavyKnight = new PlaceableMock(){ PlacementTypeToReturn = PlacementType.HeavyKnight, Cost = 3};
            var enemyReplaceable = new ReplaceableMock() { ReplaceCost = 3 };
            var factory = new PlaceableMocksFactory()
            { 
                TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
                {
                    { PlacementType.Knight, knight },
                    { PlacementType.HeavyKnight, heavyKnight },
                } 
            }; 
            var expectedList = new List<AbstractMove>()
            {
                new ReplaceMove(player, position, knight)
            };
            return new object[]{player, enemyReplaceable, listProvider, factory, position, expectedList};
        }
    }
    
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(10, 10)]
    [InlineData(0, 1)]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfNoCellOnPosition(int x, int y)
    {
        Vector2Int position = (x, y);
        var player = GetPlayer();
        var board = new Board(CellType.Square);
        board.AddCell(0, 0);
        board.AddCell(1, 1);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder(){Board = board}.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfObstacleOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var obstacle = GetObstacle();
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(obstacle);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfPositionIsFarFromPlayerUnits()
    {
        var board = GetFullNByNBoard(3);
        var player = GetPlayer();
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var position = new Vector2Int(2, 2);
        board[0, 0].AddContent(playerUnit);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);

        Assert.Empty(actualList);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfTooExpensiveUpgradeableOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 2);
        var upgradeable = new UpgradeableMock() { Owner = player, UpgradeCost = 6};
        var position = new Vector2Int(0, 0);
        board[0, 0].AddContent(upgradeable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }
    
    [Fact]
    //Valid upgradeable is upgradeable that belongs to player and is not too expensive to upgrade
    public void GetPossibleMoves_ShouldReturnListWithUpgradeMove_IfValidUpgradeableOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var upgradeable = new UpgradeableMock() { Owner = player, UpgradeCost = 2};
        var position = new Vector2Int(0, 0);
        board[0, 0].AddContent(upgradeable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Single(actualList);
        Assert.IsType<UpgradeMove>(actualList[0]);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfCapturableOnPositionIsTooExpensive()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 2);
        var enemy = GetPlayer();
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var enemyCapturable = new CapturableMock() { Owner = enemy, GetCaptureCostHitFunc = (p) => 6};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(enemyCapturable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }

    [Fact]
    //Note that capture move is possible only when there are player units on nearby cells
    public void GetPossibleMoves_ShouldReturnListWithCaptureMove_IfEnemyCapturableOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer();
        var enemy = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        var enemyCapturable = new CapturableMock() { Owner = enemy, GetCaptureCostHitFunc = (p) => 2};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(enemyCapturable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Single(actualList);
        Assert.IsType<CaptureMove>(actualList[0]);
    }

    [Theory]
    [ClassData(typeof(GetPossiblePlaceMovesTestCases))]
    public void GetPossibleMoves_ShouldReturnListOfPossiblePlaceMoves_IfNoObstaclesOnPosition(Player player, 
        IPlacementListProvider listProvider, 
        IPlaceablesFactory factory,
        Vector2Int position,  
        List<AbstractMove> expectedList)
    {
        var board = GetFullNByNBoard(2);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        board[0, 0].AddContent(playerUnit);
        var possibleMovesSelector = new PossibleMovesSelector(board, factory, listProvider);
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Equal(expectedList.Count, actualList.Count);
        foreach (var actualMove in actualList)
        {
            Assert.Contains(actualMove, expectedList);
        }
    }
    
    [Theory]
    [ClassData(typeof(GetPossibleReplaceMovesTestCases))]
    public void GetPossibleMoves_ShouldReturnListOfPossibleReplaceMoves_IfEnemyReplaceableOnPosition(Player player,
        Content enemyReplaceable,
        IPlacementListProvider listProvider, 
        IPlaceablesFactory factory,
        Vector2Int position,  
        List<AbstractMove> expectedList)
    {
        var board = GetFullNByNBoard(2);
        var playerUnit = new PlayerUnitMock() { Owner = player };
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(enemyReplaceable);
        var possibleMovesSelector = new PossibleMovesSelector(board, factory, listProvider);
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Equal(expectedList.Count, actualList.Count);
        foreach (var actualMove in actualList)
        {
            Assert.Contains(actualMove, expectedList);
        }
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfRemovableOnPositionCannotBeRemoved()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock() { Owner = player};
        var removable = new RemovableMock() { CanRemove = false };
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnEmptyList_IfTooExpensiveRemovableOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 2);
        var playerUnit = new PlayerUnitMock() { Owner = player};
        var removable = new RemovableMock() { CanRemove = true, RemoveCost = 5};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Empty(actualList);
    }

    [Fact]
    public void GetPossibleMoves_ShouldReturnListWithRemoveMove_IfRemoveIsPossibleOnPosition()
    {
        var board = GetFullNByNBoard(2);
        var player = GetPlayer(actionPoints: 6);
        var playerUnit = new PlayerUnitMock() { Owner = player};
        var removable = new RemovableMock() { CanRemove = true, RemoveCost = 2};
        var position = new Vector2Int(1, 1);
        board[0, 0].AddContent(playerUnit);
        board[position].AddContent(removable);
        var possibleMovesSelector = new PossibleMovesSelectorBuilder() { Board = board }.Build();
        
        var actualList = possibleMovesSelector.GetPossibleMoves(player, position);
        
        Assert.Single(actualList);
        Assert.IsType<RemoveMove>(actualList[0]);
    }
}