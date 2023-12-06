﻿using castledice_game_logic;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.TurnsLogic;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using Moq;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace castledice_game_logic_tests;

public static class ObjectCreationUtility
{

    public static ActionPointsTsc GetActionPointsTsc()
    {
        var currentPlayerProviderMock = new Mock<ICurrentPlayerProvider>();
        return new ActionPointsTsc(currentPlayerProviderMock.Object);
    }
    
    public static ITreesFactory GetTreesFactory()
    {
        var factoryMock = new Mock<ITreesFactory>();
        factoryMock.Setup(f => f.GetTree()).Returns(new Tree(1, false));
        return factoryMock.Object;
    }
    
    public static CastleGO GetCastle(Player player, int durability = 3, int maxDurability = 3, int maxFreeDurability = 1, int captureHitCost = 1)
    {
        return new CastleGO(player, durability, maxDurability, maxFreeDurability, captureHitCost);
    }

    public static Knight GetKnight(Player player, int health = 3, int placementCost = 1)
    {
        return new Knight(player, placementCost, health);
    }

    public static PlayerUnitMock GetUnit(Player player)
    {
        return new PlayerUnitMock() { Owner = player };
    }
    
    public static PlayerTurnsSwitcher GetTurnsSwitcher(params Player[] players)
    {
        return new PlayerTurnsSwitcher(new PlayersList(players));
    } 
    
    public static Board GetFullNByNBoard(int size)
    {
        var board = new Board(CellType.Square);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                board.AddCell(i, j);
            }
        }
        return board;
    }
    
    /// <summary>
    /// Returns cell with (0, 0) coordinates.
    /// </summary>
    /// <returns></returns>
    public static Cell GetCell()
    {
        return new Cell(new Vector2Int(0, 0));
    }
    
    public static List<Player> GetPlayersList(int length)
    {
        List<Player> players = new List<Player>();
        for (int i = 0; i < length; i++)
        {
            players.Add(GetPlayer());
        }

        return players;
    }

    public static BoardConfig GetDefaultBoardConfig(Player firstPlayer, Player secondPlayer)
    {
        var cellsGenerator = new RectCellsGenerator(10, 10);
        var castlesPlacementData = new Dictionary<Player, Vector2Int>()
        {
            { firstPlayer, new Vector2Int(0, 0) },
            { secondPlayer, new Vector2Int(9, 9) }
        };
        var castlesFactoryMock = new Mock<ICastlesFactory>();
        castlesFactoryMock.Setup(m => m.GetCastle(firstPlayer)).Returns(GetCastle(firstPlayer));
        castlesFactoryMock.Setup(m => m.GetCastle(secondPlayer)).Returns(GetCastle(secondPlayer));
        var castlesSpawner = new CastlesSpawner(castlesPlacementData, castlesFactoryMock.Object);
        var cellType = CellType.Square;
        var boardConfig = new BoardConfig(new List<IContentSpawner>() { castlesSpawner }, cellsGenerator, cellType);
        return boardConfig;
    }
    
    public static Player GetPlayer(int id = 0, int actionPoints = 6)
    {
        var playerActionPoints = new PlayerActionPoints
        {
            Amount = actionPoints,
        };
        var playerId = id;
        return new Player(playerActionPoints, playerId);
    }

    
    /// <summary>
    /// Returns some cell content that is easy to create. You should not expect any particular content.
    /// </summary>
    /// <returns></returns>
    public static Content GetCellContent()
    {
        return new ObstacleMock();
    }

    
    /// <summary>
    /// Returns cell content that blocks placement and can't be removed by player.
    /// </summary>
    /// <returns></returns>
    public static Content GetObstacle()
    {
        return new ObstacleMock();
    }

    public static IPlaceable GetPlaceable()
    {
        return new PlaceableMock();
    }

    public abstract class AbstractMoveBuilder
    {
        public Player Player = GetPlayer();
        public Vector2Int Position = new Vector2Int(0, 0);
    }
    
    public class PlaceMoveBuilder : AbstractMoveBuilder
    {

        public IPlaceable Content = GetPlaceable();
        
        public PlaceMove Build()
        {
            return new PlaceMove(Player, Position, Content);
        }
    }
    
    public class ReplaceMoveBuilder : AbstractMoveBuilder
    {
        public IPlaceable Replacement = GetPlaceable();
        
        public ReplaceMove Build()
        {
            return new ReplaceMove(Player, Position, Replacement);
        }
    }

    public class RemoveMoveBuilder : AbstractMoveBuilder
    {
        public RemoveMove Build()
        {
            return new RemoveMove(Player, Position);
        }
    }

    public class UpgradeMoveBuilder : AbstractMoveBuilder
    {
        public UpgradeMove Build()
        {
            return new UpgradeMove(Player, Position);
        }
    }

    public class CaptureMoveBuilder : AbstractMoveBuilder
    {
        public CaptureMove Build()
        {
            return new CaptureMove(Player, Position);
        }
    }

    public class TestMoveBuilder : AbstractMoveBuilder
    {
        public TestMove Build()
        {
            return new TestMove(Player, Position);
        }
    }

    internal class PossibleMovesSelectorBuilder
    {
        public Board Board = GetFullNByNBoard(3);
        public IPlaceablesFactory PlaceablesFactory = new PlaceableMocksFactory();
        public IDecksList DecksList = new DecksListMock()
        {
            ListToReturn = new List<PlacementType>() { PlacementType.Knight , PlacementType.HeavyKnight}
        };

        public PossibleMovesSelector Build()
        {
            return new PossibleMovesSelector(Board, PlaceablesFactory, DecksList);
        }
    }
}