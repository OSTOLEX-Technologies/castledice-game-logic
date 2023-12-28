using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;
using CastleGO = castledice_game_logic.GameObjects.Castle;
namespace castledice_game_logic_tests;

public class CastleSpawnerTests
{
    public static IEnumerable<object[]> CastlesSpawnDataCases()
    {
        yield return new object[]
        {
            new Dictionary<Player, Vector2Int>()
            {
                { GetPlayer(), (0, 0) },
                { GetPlayer(), (9, 9) }
            }
        };
        yield return new object[]
        {
            new Dictionary<Player, Vector2Int>()
            {
                { GetPlayer(), (3, 3) }
            }
        };
        yield return new object[]
        {
            new Dictionary<Player, Vector2Int>()
            {
                { GetPlayer(), (0, 0) },
                { GetPlayer(), (9, 9) },
                { GetPlayer(), (4, 4) }
            }
        };
    }

    private static ICastlesFactory GetFactory(Dictionary<Player, Vector2Int> castlesSpawnData)
    {
        var factoryMock = new Mock<ICastlesFactory>();
        foreach (var keyValuePair in castlesSpawnData)
        {
            factoryMock.Setup(m => m.GetCastle(keyValuePair.Key)).Returns(GetCastle(keyValuePair.Key));
        }

        return factoryMock.Object;
    }

    [Theory]
    [MemberData(nameof(CastlesSpawnDataCases))]
    public void SpawnContent_ShouldSpawnCastles_OnGivenPositions(Dictionary<Player, Vector2Int> castlesSpawnData)
    {
        var board = GetFullNByNBoard(10);
        var factory = GetFactory(castlesSpawnData);
        var castlesSpawner = new CastlesSpawner(castlesSpawnData, factory);

        castlesSpawner.SpawnContent(board);

        foreach (var keyValuePair in castlesSpawnData)
        {
            Assert.Contains(board[keyValuePair.Value].GetContent(), c => c is CastleGO);
        }
    }

    [Theory]
    [MemberData(nameof(CastlesSpawnDataCases))]
    public void SpawnContent_ShouldSpawnCastles_WithAppropriatePlayersAssigned(
        Dictionary<Player, Vector2Int> castlesSpawnData)
    {
        var board = GetFullNByNBoard(10);
        var factory = GetFactory(castlesSpawnData);
        var castlesSpawner = new CastlesSpawner(castlesSpawnData, factory);

        castlesSpawner.SpawnContent(board);

        foreach (var keyValuePair in castlesSpawnData)
        {
            var castle = board[keyValuePair.Value].GetContent().Find(c => c is CastleGO) as CastleGO;
            if (castle is null)
            {
                Assert.Fail("Castle is null on position: " + keyValuePair.Value);
            }

            Assert.Same(keyValuePair.Key, castle.GetOwner());
        }
    }

    [Theory]
    [MemberData(nameof(CastlesSpawnDataCases))]
    public void CastlesPlacementsDataProperty_ShouldReturnAShallowCopyOfDictionary_GivenInConstructor(
        Dictionary<Player, Vector2Int> castlesSpawnData)
    {
        var factory = GetFactory(castlesSpawnData);
        var castlesSpawner = new CastlesSpawner(castlesSpawnData, factory);

        var castlesPlacementsData = castlesSpawner.CastlesPlacementsData;

        Assert.NotSame(castlesSpawnData, castlesPlacementsData);
        Assert.Equal(castlesSpawnData, castlesPlacementsData);
    }

    public static IEnumerable<object> CastlePlacementDataPropertyTestCases()
    {
        yield return new []
        {
            new Dictionary<Player, Vector2Int>
            {
                { GetPlayer(1), (0, 0) },
                { GetPlayer(2), (9, 9) }
            }
        };
        yield return new[]
        {
            new Dictionary<Player, Vector2Int>
            {
                { GetPlayer(3), (0, 0) },
                { GetPlayer(4), (9, 9) },
                { GetPlayer(5), (4, 4) }
            }
        };
    }

    [Fact]
    public void FactoryProperty_ShouldReturnFactory_GivenInConstructor()
    {
        var expectedFactory = new Mock<ICastlesFactory>();
        var castlesSpawner = new CastlesSpawner(new Dictionary<Player, Vector2Int>(), expectedFactory.Object);
        
        Assert.Same(expectedFactory.Object, castlesSpawner.Factory);
    }
}