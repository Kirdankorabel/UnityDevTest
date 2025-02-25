public class GameModel
{
    private int _count;

    public event System.Action<int> OnCountUpdated;

    public int Count => _count;

    public void AddPoitns(int count)
    {
        _count += count;
        OnCountUpdated?.Invoke(_count);
    }
}
