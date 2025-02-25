public class BallModel
{
    private int _counter;
    private bool _isCounted;

    public event System.Action<int> OnUpdate;

    public int Counter => _counter; 
    public bool IsCounted => _isCounted;

    public void Count()
    {
        _isCounted = true;
    }

    public void AddPoint()
    {
        _counter++;
        OnUpdate?.Invoke(_counter);
    }
}
