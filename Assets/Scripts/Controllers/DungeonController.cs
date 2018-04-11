using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField]
    private int SizeX = 10;
    [SerializeField]
    private int SizeY = 10;
    [SerializeField]
    private int StartPosX = 0;
    [SerializeField]
    private int StartPosY = 0;
    [SerializeField]
    private int NumberOfRooms = 40;
    [SerializeField]
    private int NumberOfBosses = 0;
    [SerializeField]
    private int NumberOfShops = 0;
    [SerializeField]
    private int NumberOfHidden = 0;
    [SerializeField]
    private int NumberOfTreasure = 0;
    [SerializeField]
    private int NumberOfLocked = 0;
    [SerializeField]
    private int LengthOfFirstPart = 20;
    [SerializeField]
    private int ChanceToConnection = 0;
    [SerializeField]
    private string Seed;
    [SerializeField]
    private GameObject EmptyRoom;
    [SerializeField]
    private GameObject StandardRoom;
    [SerializeField]
    private GameObject BossRoom;
    [SerializeField]
    private GameObject ShopRoom;
    [SerializeField]
    private GameObject HiddenRoom;
    [SerializeField]
    private GameObject TreasureRoom;
    [SerializeField]
    private GameObject LockedRoom;
    [SerializeField]
    private GameObject ConnectionHorizontal;
    [SerializeField]
    private GameObject ConnectionVertical;
    [SerializeField]
    private GameObject ConnectionHorizontalHidden;
    [SerializeField]
    private GameObject ConnectionVerticalHidden;
    [SerializeField]
    private GameObject ConnectionHorizontalLocked;
    [SerializeField]
    private GameObject ConnectionVerticalLocked;

    private DungeonGenerator Dungeon;

    void Start()
    {
        CreateDungeon();
        ShowDungeon();
    }

    private void CreateDungeon()
    {
        if (LengthOfFirstPart > NumberOfRooms)
        {
            LengthOfFirstPart = NumberOfRooms;
        }

        Dungeon = new DungeonGenerator(SizeX, SizeY, NumberOfRooms);

        Dungeon.RandomGenerator = new System.Random(Seed.GetHashCode());
        Dungeon.ChanceToConnection = ChanceToConnection;
        Dungeon.LengthOfFirstPart = LengthOfFirstPart;
        Dungeon.NumberOfBosses = NumberOfBosses;
        Dungeon.NumberOfHidden = NumberOfHidden;
        Dungeon.NumberOfLocked = NumberOfLocked;
        Dungeon.NumberOfShops = NumberOfShops;
        Dungeon.NumberOfTreasure = NumberOfTreasure;
        Dungeon.StartPosX = StartPosX;
        Dungeon.StartPosY = StartPosY;

        Dungeon.Create();
    }

    private void ShowDungeon()
    {
        float OffsetX = 5.2f;
        float OffsetY = 4.8f;
        int t = 0, u = 0;
        float move = 0.25f;

        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                if (Dungeon.RoomGrid[j, i].Type == RoomType.Boss)
                    Instantiate(BossRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.Shop)
                    Instantiate(ShopRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.Hidden)
                    Instantiate(HiddenRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.Treasure)
                    Instantiate(TreasureRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.Locked)
                    Instantiate(LockedRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.Normal)
                    Instantiate(StandardRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);
                else if (Dungeon.RoomGrid[j, i].Type == RoomType.NoRoom)
                    Instantiate(EmptyRoom, new Vector3((j + u) * move + OffsetX, (i + t) * -move + OffsetY, -1), Quaternion.identity);

                if (Dungeon.RoomGrid[j, i].RightConnection != ConnectionType.NoConnection)
                {
                    if (Dungeon.RoomGrid[j, i].RightConnection == ConnectionType.Normal)
                        Instantiate(ConnectionHorizontal, new Vector3((j + u + 1) * move + OffsetX, (i + t) * -move + OffsetY, 0), Quaternion.identity);
                    if (Dungeon.RoomGrid[j, i].RightConnection == ConnectionType.Hidden)
                        Instantiate(ConnectionHorizontalHidden, new Vector3((j + u + 1) * move + OffsetX, (i + t) * -move + OffsetY, 0), Quaternion.identity);
                    if (Dungeon.RoomGrid[j, i].RightConnection == ConnectionType.Locked)
                        Instantiate(ConnectionHorizontalLocked, new Vector3((j + u + 1) * move + OffsetX, (i + t) * -move + OffsetY, 0), Quaternion.identity);

                }
                if (Dungeon.RoomGrid[j, i].DownConnection != ConnectionType.NoConnection)
                {
                    if (Dungeon.RoomGrid[j, i].DownConnection == ConnectionType.Normal)
                        Instantiate(ConnectionVertical, new Vector3((j + u) * move + OffsetX, (i + t + 1) * -move + OffsetY, 0), Quaternion.identity);
                    if (Dungeon.RoomGrid[j, i].DownConnection == ConnectionType.Hidden)
                        Instantiate(ConnectionVerticalHidden, new Vector3((j + u) * move + OffsetX, (i + t + 1) * -move + OffsetY, 0), Quaternion.identity);
                    if (Dungeon.RoomGrid[j, i].DownConnection == ConnectionType.Locked)
                        Instantiate(ConnectionVerticalLocked, new Vector3((j + u) * move + OffsetX, (i + t + 1) * -move + OffsetY, 0), Quaternion.identity);
                }
                u++;
            }
            t++;
            u = 0;
        }
    }
}
