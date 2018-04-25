public class UseDoor : Interactable
{
    public Direction direction;

    bool WasUsed = false;

    public override void Interract()
    {
        if (!WasUsed && PlayerControler.Instance.CanUseDoor)
            Use();
    }

    private void Use()
    {
        WasUsed = true;
        GameController.Instance.MoveToRoom(direction);
    }
}
