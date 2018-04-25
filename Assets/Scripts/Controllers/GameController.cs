using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private float RoomMovementSpeed = 3f;
    [SerializeField]
    private float WallOffset = 16.6f;
    [SerializeField]
    private float PlayerOffset = 0.95f;

    [SerializeField]
    private string Seed = "a";

    [SerializeField]
    private List<GameObject> WallsHorizontal;
    [SerializeField]
    private List<GameObject> WallsVertical;

    [SerializeField]
    private List<GameObject> DoorsHorizontal;
    [SerializeField]
    private List<GameObject> DoorsVertical;

    [SerializeField]
    private List<GameObject> Floors;

    private GameObject CurrentRoom;
    private GameObject NextRoom;

    private int DungeonPosX;
    private int DungeonPosY;

    private Room[,] RoomGrid;

    private System.Random RandomGenerator;

    private Direction SpawnedRoomDirection = Direction.NoDirection;

    public bool IsNextRoomMoving = false;

    private Vector3 PlayerDestination; 

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RandomGenerator = new System.Random(Seed.GetHashCode());
        CreateRoomGrid();
        DefineRoom(DungeonPosX, DungeonPosY);
        ShowRoom(DungeonPosX, DungeonPosY);
    }

    private void FixedUpdate()
    {
        if (IsNextRoomMoving)
        {
            CurrentRoom.transform.position = Vector3.MoveTowards(CurrentRoom.transform.position, DirectionToNegativeVectorConverter(SpawnedRoomDirection, WallOffset * 2), RoomMovementSpeed * Time.deltaTime);
            NextRoom.transform.position = Vector3.MoveTowards(NextRoom.transform.position, Vector3.zero, RoomMovementSpeed * Time.deltaTime);

            if (Vector3.Distance(NextRoom.transform.position, Vector3.zero) == 0)
            {
                Destroy(CurrentRoom);
                CurrentRoom = NextRoom;
                IsNextRoomMoving = false;
            }
        }
    }

    private void CreateRoomGrid()
    {
        DungeonGenerator generator = new DungeonGenerator(10, 10, 50);
        generator.RandomGenerator = RandomGenerator;
        generator.ChanceToConnection = 20;
        generator.LengthOfFirstPart = 25;
        generator.NumberOfBosses = 1;
        generator.NumberOfHidden = 1;
        generator.NumberOfLocked = 1;
        generator.NumberOfShops = 1;
        generator.NumberOfTreasure = 1;

        RoomGrid = generator.Create();

        DungeonPosX = generator.StartPosX;
        DungeonPosY = generator.StartPosY;
    }

    public void MoveToRoom(Direction direction)
    {
        UpdatePosition(ref DungeonPosX, ref DungeonPosY, direction);

        if (!RoomGrid[DungeonPosX, DungeonPosY].WasVisited)
            DefineRoom(DungeonPosX, DungeonPosY);

        ShowRoom(DungeonPosX, DungeonPosY, direction);
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
    private Vector3 StartPlayerPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Vector3(-PlayerOffset, 0, 0);
            case Direction.Up:
                return new Vector3(0, PlayerOffset, 0);
            case Direction.Right:
                return new Vector3(PlayerOffset, 0, 0);
            case Direction.Down:
                return new Vector3(0, -PlayerOffset, 0);
            default:
                return Vector3.zero;
        }
    }

    private Vector3 DirectionToNegativeVectorConverter(Direction direction, float vectorLength)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Vector3(vectorLength, 0, 0);
            case Direction.Up:
                return new Vector3(0, -vectorLength, 0);
            case Direction.Right:
                return new Vector3(-vectorLength, 0, 0);
            case Direction.Down:
                return new Vector3(0, vectorLength, 0);
            default:
                return Vector3.zero;
        }
    }
    private Vector3 DirectionToVectorConverter(Direction direction, float vectorLength)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Vector3(-vectorLength, 0, 0);
            case Direction.Up:
                return new Vector3(0, vectorLength, 0);
            case Direction.Right:
                return new Vector3(vectorLength, 0, 0);
            case Direction.Down:
                return new Vector3(0, -vectorLength, 0);
            default:
                return Vector3.zero;
        }
    }

    private void DefineRoom(int x, int y)
    {
        Room newRoom = new Room(RoomGrid[x, y]);

        newRoom.Floor = RandomGenerator.Next(Floors.Count - 1);

        newRoom.LeftWall[0] = RandomGenerator.Next(WallsVertical.Count - 1);
        newRoom.RightWall[0] = RandomGenerator.Next(WallsVertical.Count - 1);

        newRoom.UpWall[0] = RandomGenerator.Next(WallsHorizontal.Count - 1);
        newRoom.DownWall[0] = RandomGenerator.Next(WallsHorizontal.Count - 1);

        if (newRoom.RightConnection != ConnectionType.NoConnection)
        {
            newRoom.RightWall[1] = RandomGenerator.Next(DoorsVertical.Count - 1);
        }
        if (newRoom.LeftConnection != ConnectionType.NoConnection)
        {
            newRoom.LeftWall[1] = RandomGenerator.Next(DoorsVertical.Count - 1);
        }
        if (newRoom.UpConnection != ConnectionType.NoConnection)
        {
            newRoom.UpWall[1] = RandomGenerator.Next(DoorsHorizontal.Count - 1);
        }
        if (newRoom.DownConnection != ConnectionType.NoConnection)
        {
            newRoom.DownWall[1] = RandomGenerator.Next(DoorsHorizontal.Count - 1);
        }

        newRoom.WasVisited = true;

        RoomGrid[x, y] = newRoom;
    }

    private void ShowRoom(int x, int y, Direction direction = Direction.NoDirection)
    {
        Room room = RoomGrid[x, y];
        GameObject roomObj;

        roomObj = new GameObject("Room Object");

        Instantiate(Floors[room.Floor.GetValueOrDefault()], new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(roomObj.transform);

        GameObject RightWall = Instantiate(WallsVertical[room.RightWall[0].GetValueOrDefault()], new Vector3(WallOffset, 0, 0), Quaternion.identity);
        RightWall.transform.SetParent(roomObj.transform);

        if (room.RightWall[1] != null)
        {
            GameObject RightDoor = Instantiate(DoorsVertical[room.RightWall[1].GetValueOrDefault()], new Vector3(WallOffset, 0, 0), Quaternion.identity);
            RightDoor.transform.SetParent(RightWall.transform);
            RightDoor.GetComponent<UseDoor>().direction = Direction.Right;
        }
        else
        {
            RightWall.AddComponent<BoxCollider2D>();
        }

        GameObject LeftWall = Instantiate(WallsVertical[room.LeftWall[0].GetValueOrDefault()], new Vector3(-WallOffset, 0, 0), Quaternion.identity);
        LeftWall.transform.SetParent(roomObj.transform);

        if (room.LeftWall[1] != null)
        {
            GameObject LeftDoor = Instantiate(DoorsVertical[room.LeftWall[1].GetValueOrDefault()], new Vector3(-WallOffset, 0, 0), Quaternion.identity);
            LeftDoor.transform.SetParent(LeftWall.transform);
            LeftDoor.GetComponent<UseDoor>().direction = Direction.Left;
        }
        else
        {
            LeftWall.AddComponent<BoxCollider2D>();
        }

        GameObject UpWall = Instantiate(WallsHorizontal[room.UpWall[0].GetValueOrDefault()], new Vector3(0, WallOffset, 0), Quaternion.identity);
        UpWall.transform.SetParent(roomObj.transform);

        if (room.UpWall[1] != null)
        {
            GameObject UpDoor = Instantiate(DoorsHorizontal[room.UpWall[1].GetValueOrDefault()], new Vector3(0, WallOffset, 0), Quaternion.identity);
            UpDoor.transform.SetParent(UpWall.transform);
            UpDoor.GetComponent<UseDoor>().direction = Direction.Up;
        }
        else
        {
            UpWall.AddComponent<BoxCollider2D>();
        }

        GameObject DownWall = Instantiate(WallsHorizontal[room.DownWall[0].GetValueOrDefault()], new Vector3(0, -WallOffset, 0), Quaternion.identity);
        DownWall.transform.SetParent(roomObj.transform);

        if (room.DownWall[1] != null)
        {
            GameObject DownDoor = Instantiate(DoorsHorizontal[room.DownWall[1].GetValueOrDefault()], new Vector3(0, -WallOffset, 0), Quaternion.identity);
            DownDoor.transform.SetParent(DownWall.transform);
            DownDoor.GetComponent<UseDoor>().direction = Direction.Down;
        }
        else
        {
            DownWall.AddComponent<BoxCollider2D>();
        }

        if (direction == Direction.NoDirection)
        {
            CurrentRoom = Instantiate(roomObj, Vector3.zero, Quaternion.identity);
        }
        else
        {
            NextRoom = Instantiate(roomObj, DirectionToVectorConverter(direction, WallOffset * 2), Quaternion.identity);
            IsNextRoomMoving = true;
            SpawnedRoomDirection = direction;
        }
        if (roomObj)
            Destroy(roomObj);
    }
}
