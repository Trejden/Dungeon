using System;
using System.Collections.Generic;
using UnityEngine;

class DungeonGenerator
{
    #region Properties

    public int StartPosX { get; set; }
    public int StartPosY { get; set; }

    public int MaxSizeX { get; set; }
    public int MaxSizeY { get; set; }

    public int NumberOfRooms { get; set; }
    public int NumberOfHidden { get; set; }
    public int NumberOfShops { get; set; }
    public int NumberOfBosses { get; set; }
    public int NumberOfLocked { get; set; }
    public int NumberOfTreasure { get; set; }

    public int ChanceToConnection { get; set; }
    public int LengthOfFirstPart { get; set; }

    public Room[,] RoomGrid { get; set; }

    public System.Random RandomGenerator { get; set; }

    #endregion

    #region Ctor

    public DungeonGenerator(int maxSizeX, int maxSizeY, int numberOfRooms)
    {
        StartPosX = 0;
        StartPosY = 0;

        MaxSizeX = maxSizeX;
        MaxSizeY = maxSizeY;

        NumberOfRooms = numberOfRooms;
        NumberOfHidden = 0;
        NumberOfShops = 0;
        NumberOfBosses = 0;
        NumberOfLocked = 0;
        NumberOfTreasure = 0;

        ChanceToConnection = 50;
        LengthOfFirstPart = numberOfRooms / 2;

        ClearRoomGrid();

        RandomGenerator = new System.Random(Guid.NewGuid().GetHashCode());
    }

    #endregion

    #region Public Methods

    public Room[,] Create()
    {
        int currentPosX = StartPosX;
        int currentPosY = StartPosY;
        int numberOfNormalRoomsToGenerate = NumberOfRooms - NumberOfLocked - NumberOfHidden;
        int currentNumberOfGeneratedRooms = 0;

        Stack<int[]> positionHistoryStack = new Stack<int[]>();
        int[,] allVisitedPositions = new int[NumberOfRooms, 2];

        ClearRoomGrid();

        allVisitedPositions[0, 0] = StartPosX;
        allVisitedPositions[0, 1] = StartPosY;
        positionHistoryStack.Push(new[] { StartPosX, StartPosY });
        CreateNewRoom(StartPosX, StartPosY);

        while (currentNumberOfGeneratedRooms < numberOfNormalRoomsToGenerate)
        {
            Direction currentDirection = DrawDirection(currentPosX, currentPosY);

            CreateNewRoom(currentPosX, currentPosY, currentDirection);
            UpdatePosition(ref currentPosX, ref currentPosY, currentDirection);
            CreateRandomConnections(currentPosX, currentPosY);

            positionHistoryStack.Push(new[] { currentPosX, currentPosY });

            allVisitedPositions[currentNumberOfGeneratedRooms, 0] = currentPosX;
            allVisitedPositions[currentNumberOfGeneratedRooms, 1] = currentPosY;

            if (currentNumberOfGeneratedRooms == LengthOfFirstPart)
                GenerateSpecialRooms(allVisitedPositions, true);

            while (AllRoomsAroundExist(currentPosX, currentPosY) && positionHistoryStack.Count > 0)
            {
                int[] pos = positionHistoryStack.Pop();

                currentPosX = pos[0];
                currentPosY = pos[1];
            }

            currentNumberOfGeneratedRooms++;
        }
        GenerateSpecialRooms(allVisitedPositions, false);

        return RoomGrid;
    }

    #endregion

    #region Private Methods

    private void ClearRoomGrid()
    {
        RoomGrid = new Room[MaxSizeX, MaxSizeY];
        for (int i = 0; i < MaxSizeX; i++)
        {
            for (int j = 0; j < MaxSizeY; j++)
            {
                RoomGrid[i, j] = new Room();
            }
        }
    }
    private void GenerateSpecialRooms(int[,] positionHistory, bool isFirstPart)
    {
        foreach (RoomType type in Enum.GetValues(typeof(RoomType)))
        {
            GenerateSpecialTypeOfRoom(type, positionHistory, isFirstPart);
        }
    }
    private void GenerateSpecialTypeOfRoom(RoomType type, int[,] positionHistory, bool isFirstPart)
    {
        int roomCounter = 0;
        int maxGeneratedRooms = GetNumberOfRoomsToGenerate(type);
        while (roomCounter < maxGeneratedRooms)
        {
            int randomNumber = 0;
            int posX = 0;
            int posY = 0;
            switch (type)
            {
                case RoomType.Hidden:
                case RoomType.Locked:
                    List<int[]> list = GetListOfPlacesForSpecialRooms(positionHistory, isFirstPart); //TODO: poprawić ustawianie ukrytych i zablokowanych pokojow
                    if (list.Count > 0)
                    {
                        randomNumber = RandomGenerator.Next(list.Count);
                        posX = list[randomNumber][0];
                        posY = list[randomNumber][1];
                        CreateNewRoom(posX, posY, roomType: type);
                        CreateSpecialConnection(posX, posY);
                    }
                    else
                    {
                    }
                    break;
                case RoomType.Boss:
                case RoomType.Shop:
                case RoomType.Treasure:
                    do
                    {
                        randomNumber = isFirstPart ? RandomGenerator.Next(LengthOfFirstPart) : RandomGenerator.Next(positionHistory.GetLength(0));
                        posX = positionHistory[randomNumber, 0];
                        posY = positionHistory[randomNumber, 1];
                    } while (RoomGrid[posX, posY].Type != RoomType.Normal);
                    CreateNewRoom(posX, posY, roomType: type);
                    break;
            }
            roomCounter++;
        }
    }
    private void CreateSpecialConnection(int posX, int posY)
    {
        Direction[] availableDirections = GetAvailableDirectionsToCreateNewConnection(posX, posY);
        Direction drawnDirection = availableDirections[RandomGenerator.Next(availableDirections.Length)];

        CreateConnection(posX, posY, drawnDirection);
    }
    private void UpdatePosition(ref int posX, ref int posY, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                posX--;
                break;
            case Direction.Up:
                posY--;
                break;
            case Direction.Right:
                posX++;
                break;
            case Direction.Down:
                posY++;
                break;
        }
    }
    private void CreateNewRoom(int posX, int posY, Direction direction = Direction.NoDirection, RoomType roomType = RoomType.Normal)
    {
        switch (direction)
        {
            case Direction.Left:
                RoomGrid[posX - 1, posY].Type = roomType;
                break;
            case Direction.Up:
                RoomGrid[posX, posY - 1].Type = roomType;
                break;
            case Direction.Right:
                RoomGrid[posX + 1, posY].Type = roomType;
                break;
            case Direction.Down:
                RoomGrid[posX, posY + 1].Type = roomType;
                break;
            case Direction.NoDirection:
                RoomGrid[posX, posY].Type = roomType;
                break;
        }
        CreateConnection(posX, posY, direction);
    }
    private void CreateRandomConnections(int posX, int posY)
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (IsOkToCreateNewConnection(posX, posY, direction))
                CreateConnection(posX, posY, direction, ChanceToConnection);
        }
    }
    private void CreateConnection(int posX, int posY, Direction direction, int chanceToConnection = 100)
    {
        int drawnNumber = RandomGenerator.Next(100);

        if (drawnNumber < chanceToConnection && direction != Direction.NoDirection)
        {
            ConnectionType connectionType = DefineConnectionType(posX, posY, direction);

            if (connectionType != ConnectionType.NoConnection)
            {
                switch (direction)
                {
                    case Direction.Left:
                        RoomGrid[posX, posY].LeftConnection = connectionType;
                        RoomGrid[posX - 1, posY].RightConnection = connectionType;
                        break;
                    case Direction.Up:
                        RoomGrid[posX, posY].UpConnection = connectionType;
                        RoomGrid[posX, posY - 1].DownConnection = connectionType;
                        break;
                    case Direction.Right:
                        RoomGrid[posX, posY].RightConnection = connectionType;
                        RoomGrid[posX + 1, posY].LeftConnection = connectionType;
                        break;
                    case Direction.Down:
                        RoomGrid[posX, posY].DownConnection = connectionType;
                        RoomGrid[posX, posY + 1].UpConnection = connectionType;
                        break;
                }
            }
        }
    }
    private int GetNumberOfRoomsToGenerate(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.Hidden:
                return NumberOfHidden / 2;
            case RoomType.Locked:
                return NumberOfLocked / 2;
            case RoomType.Boss:
                return NumberOfBosses / 2;
            case RoomType.Shop:
                return NumberOfShops / 2;
            case RoomType.Treasure:
                return NumberOfTreasure / 2;
            default:
                return 0;
        }
    }
    private int[] GetArrayOfPosition(int posX, int posY, Direction direction)
    {
        UpdatePosition(ref posX, ref posY, direction);

        return new int[] { posX, posY };
    }
    private List<int[]> GetListOfPlacesForSpecialRooms(int[,] positionHistory, bool isFirstPart)
    {
        int startPoint = isFirstPart ? 0 : LengthOfFirstPart;
        int endPoint = isFirstPart ? LengthOfFirstPart : positionHistory.GetLength(0) - 1;

        List<int[]> listOfPositions = new List<int[]>();

        while (startPoint < endPoint)
        {
            int posX = positionHistory[endPoint, 0];
            int posY = positionHistory[endPoint, 1];

            Direction[] availableDirections = GetAvailableDirectionsToCreateNewRoom(posX, posY);

            foreach (Direction direction in availableDirections)
            {
                listOfPositions.Add(GetArrayOfPosition(posX, posY, direction));
            }

            endPoint--;

            if (endPoint == startPoint && listOfPositions.Count == 0 && startPoint > 0)
            {
                if (startPoint >= 50)
                    startPoint -= 50;
                else
                    startPoint = 0;
            }
        }
        return listOfPositions;
    }
    private RoomType GetRoomType(int posX, int posY, Direction direction = Direction.NoDirection)
    {
        UpdatePosition(ref posX, ref posY, direction);

        if (!IsEndOfArray(posX, posY))
            return RoomGrid[posX, posY].Type;
        else
            return RoomType.NoRoom;
    }
    private ConnectionType DefineConnectionType(int posX, int posY, Direction direction)
    {
        RoomType typeOne = GetRoomType(posX, posY);
        RoomType typeTwo = GetRoomType(posX, posY, direction);

        ConnectionType connOne = TransformRoomTypeToConnection(typeOne);
        ConnectionType connTwo = TransformRoomTypeToConnection(typeTwo);

        if (connOne != ConnectionType.NoConnection && connOne != ConnectionType.Normal)
            return connOne;
        else if (connTwo != ConnectionType.NoConnection && connTwo != ConnectionType.Normal)
            return connOne;
        else if (connOne == ConnectionType.Normal && connTwo == ConnectionType.Normal)
            return ConnectionType.Normal;
        else
            return ConnectionType.NoConnection;
    }
    private ConnectionType TransformRoomTypeToConnection(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.Boss:
            case RoomType.Normal:
            case RoomType.Shop:
            case RoomType.Treasure:
                return ConnectionType.Normal;
            case RoomType.Hidden:
                return ConnectionType.Hidden;
            case RoomType.Locked:
                return ConnectionType.Locked;
            default:
                return ConnectionType.NoConnection;
        }
    }
    private Direction DrawDirection(int posX, int posY)
    {
        Array availableDirections = GetAvailableDirectionsToCreateNewRoom(posX, posY);
        if (availableDirections.Length > 0)
            return (Direction)availableDirections.GetValue(RandomGenerator.Next(availableDirections.Length));
        else
            return Direction.NoDirection;
    }
    private Direction[] GetAvailableDirectionsToCreateNewRoom(int posX, int posY)
    {
        List<Direction> availableDirections = new List<Direction>();

        if (IsOkToMakeNewRoom(posX - 1, posY))
            availableDirections.Add(Direction.Left);
        if (IsOkToMakeNewRoom(posX, posY - 1))
            availableDirections.Add(Direction.Up);
        if (IsOkToMakeNewRoom(posX + 1, posY))
            availableDirections.Add(Direction.Right);
        if (IsOkToMakeNewRoom(posX, posY + 1))
            availableDirections.Add(Direction.Down);

        return availableDirections.ToArray();
    }
    private Direction[] GetAvailableDirectionsToCreateNewConnection(int posX, int posY)
    {
        List<Direction> availableDirections = new List<Direction>();

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (IsOkToCreateNewConnection(posX, posY, direction))
                availableDirections.Add(direction);
        }
        return availableDirections.ToArray();
    }
    private bool AllRoomsAroundExist(int posX, int posY)
    {
        return (IsEndOfArray(posX - 1, posY) || IsRoomOnPosition(posX - 1, posY)) &&
               (IsEndOfArray(posX, posY - 1) || IsRoomOnPosition(posX, posY - 1)) &&
               (IsEndOfArray(posX + 1, posY) || IsRoomOnPosition(posX + 1, posY)) &&
               (IsEndOfArray(posX, posY + 1) || IsRoomOnPosition(posX, posY + 1));
    }
    private bool IsOkToMakeNewRoom(int posX, int posY)
    {
        return !IsEndOfArray(posX, posY) && RoomGrid[posX, posY].Type == RoomType.NoRoom;
    }
    private bool IsRoomOnPosition(int posX, int posY)
    {
        return !IsEndOfArray(posX, posY) && RoomGrid[posX, posY].Type != RoomType.NoRoom;
    }
    private bool IsEndOfArray(int posX, int posY)
    {
        return !(posX >= 0 && posX < MaxSizeX && posY >= 0 && posY < MaxSizeY);
    }
    private bool ConnectionExists(int posX, int posY, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return !IsEndOfArray(posX - 1, posY) && RoomGrid[posX, posY].LeftConnection != ConnectionType.NoConnection;
            case Direction.Up:
                return !IsEndOfArray(posX, posY - 1) && RoomGrid[posX, posY].UpConnection != ConnectionType.NoConnection;
            case Direction.Right:
                return !IsEndOfArray(posX + 1, posY) && RoomGrid[posX, posY].RightConnection != ConnectionType.NoConnection;
            case Direction.Down:
                return !IsEndOfArray(posX, posY + 1) && RoomGrid[posX, posY].DownConnection != ConnectionType.NoConnection;
            default:
                return false;
        }
    }
    private bool IsOkToCreateNewConnection(int posX, int posY, Direction direction)
    {
        if (direction == Direction.NoDirection)
            return false;

        bool connExists = ConnectionExists(posX, posY, direction);
        UpdatePosition(ref posX, ref posY, direction);
        bool roomExists = IsRoomOnPosition(posX, posY);

        RoomType roomType = GetRoomType(posX, posY);

        return !connExists && roomExists && roomType != RoomType.Locked && roomType != RoomType.Hidden;
    }

    #endregion
}
