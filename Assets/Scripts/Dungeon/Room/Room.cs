using UnityEngine;

public class Room
{
    public ConnectionType LeftConnection { get; set; }
    public ConnectionType RightConnection { get; set; }
    public ConnectionType UpConnection { get; set; }
    public ConnectionType DownConnection { get; set; }

    public int? Floor { get; set; }

    public int?[] UpWall { get; set; }
    public int?[] RightWall { get; set; }
    public int?[] DownWall { get; set; }
    public int?[] LeftWall { get; set; }

    public RoomType Type { get; set; }

    public bool WasVisited { get; set; }

    public Room()
    {
        UpWall = new int?[2];
        RightWall = new int?[2];
        DownWall = new int?[2];
        LeftWall = new int?[2];
    }

    public Room(Room room)
    {
        LeftConnection = room.LeftConnection;
        RightConnection = room.RightConnection;
        UpConnection = room.UpConnection;
        DownConnection = room.DownConnection;

        Floor = room.Floor;

        LeftWall = room.LeftWall;
        RightWall = room.RightWall;
        UpWall = room.UpWall;
        DownWall = room.DownWall;

        Type = room.Type;

        WasVisited = room.WasVisited;
    }
}
