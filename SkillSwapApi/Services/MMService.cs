namespace SkillSwapAPI.Services;

public class MMService
{
    private readonly List<string> _queue = new List<string>();

    public void AddToQueue(string playerName)
    {
        if (!_queue.Contains(playerName))
        {
            _queue.Add(playerName);
        }
    }

    public List<string> GetQueue() => _queue;
}