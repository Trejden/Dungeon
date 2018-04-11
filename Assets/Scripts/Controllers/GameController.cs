using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int DungeonPosX;
    public int DungeonPosY;

    public Room[,] RoomGrid;

    public List<GameObject> WallsHorizontal;
    public List<GameObject> WallsVertical;

    public List<GameObject> DoorsHorizontal;
    public List<GameObject> DoorsVertical;

    public List<GameObject> Floors;

    public static GameController Instance;

    private GameObject RoomObject;

    System.Random RandomGenerator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RandomGenerator = new System.Random("a".GetHashCode());
        CreateRoomGrid();
        DefineRoom(DungeonPosX, DungeonPosY);
        ShowRoom(DungeonPosX, DungeonPosY);
    }

    void Update()
    {

    }

    public void MoveToRoom(Direction direction)
    {
        UpdatePosition(ref DungeonPosX, ref DungeonPosY, direction);
        if (!RoomGrid[DungeonPosX, DungeonPosY].WasVisited)
            DefineRoom(DungeonPosX, DungeonPosY);

        ShowRoom(DungeonPosX, DungeonPosY);
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
    private void CreateRoomGrid()
    {
        DungeonGenerator generator = new DungeonGenerator(10, 10, 100);
        generator.RandomGenerator = RandomGenerator;
        generator.ChanceToConnection = 20;
        generator.LengthOfFirstPart = 50;

        generator.Create();

        RoomGrid = generator.RoomGrid;

        DungeonPosX = generator.StartPosX;
        DungeonPosY = generator.StartPosY;
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

    private void ShowRoom(int x, int y)
    {
        Room room = RoomGrid[x, y];

        PlayerControler.Instance.ResetPos();

        if (RoomObject != null)
            Destroy(RoomObject);

        RoomObject = new GameObject("Room Object");

        Instantiate(Floors[room.Floor.GetValueOrDefault()], new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(RoomObject.transform);

        GameObject RightWall = Instantiate(WallsVertical[room.RightWall[0].GetValueOrDefault()], new Vector3(16.55f, 0, 0), Quaternion.identity);
        RightWall.transform.SetParent(RoomObject.transform);

        if (room.RightWall[1] != null)
        {
            GameObject RightDoor = Instantiate(DoorsVertical[room.RightWall[1].GetValueOrDefault()], new Vector3(16.55f, 0, 0), Quaternion.identity);
            RightDoor.transform.SetParent(RightWall.transform);
            RightDoor.GetComponent<UseDoor>().direction = Direction.Right;
        }

        GameObject LeftWall = Instantiate(WallsVertical[room.LeftWall[0].GetValueOrDefault()], new Vector3(-16.55f, 0, 0), Quaternion.identity);
        LeftWall.transform.SetParent(RoomObject.transform);

        if (room.LeftWall[1] != null)
        {
            GameObject LeftDoor = Instantiate(DoorsVertical[room.LeftWall[1].GetValueOrDefault()], new Vector3(-16.55f, 0, 0), Quaternion.identity);
            LeftDoor.transform.SetParent(LeftWall.transform);
            LeftDoor.GetComponent<UseDoor>().direction = Direction.Left;
        }

        GameObject UpWall = Instantiate(WallsHorizontal[room.UpWall[0].GetValueOrDefault()], new Vector3(0, 16.55f, 0), Quaternion.identity);
        UpWall.transform.SetParent(RoomObject.transform);

        if (room.UpWall[1] != null)
        {
            GameObject UpDoor = Instantiate(DoorsHorizontal[room.UpWall[1].GetValueOrDefault()], new Vector3(0, 16.55f, 0), Quaternion.identity);
            UpDoor.transform.SetParent(UpWall.transform);
            UpDoor.GetComponent<UseDoor>().direction = Direction.Up;
        }

        GameObject DownWall = Instantiate(WallsHorizontal[room.DownWall[0].GetValueOrDefault()], new Vector3(0, -16.55f, 0), Quaternion.identity);
        DownWall.transform.SetParent(RoomObject.transform);

        if (room.DownWall[1] != null)
        {
            GameObject DownDoor = Instantiate(DoorsHorizontal[room.DownWall[1].GetValueOrDefault()], new Vector3(0, -16.55f, 0), Quaternion.identity);
            DownDoor.transform.SetParent(DownWall.transform);
            DownDoor.GetComponent<UseDoor>().direction = Direction.Down;
        }
    }
}
