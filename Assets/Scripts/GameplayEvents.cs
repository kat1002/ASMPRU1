using UnityEngine;
using UnityEngine.Events;

public class GameplayEvents
{
    private readonly static UnityEvent _onGameOver = new UnityEvent();
    private readonly static UnityEvent _onGamePause = new UnityEvent();
    private readonly static UnityEvent _onGameUnPause = new UnityEvent();
    private readonly static UnityEvent<int> _onEarnScore = new UnityEvent<int>();
    private readonly static UnityEvent<int> _onUpdateScore = new UnityEvent<int>();
    private readonly static UnityEvent<Transform> _onExplosion = new UnityEvent<Transform>();

    public static UnityEvent OnGameOver { get => _onGameOver; }
    public static UnityEvent OnGamePause { get => _onGamePause; }
    public static UnityEvent OnGameUnPause { get => _onGameUnPause; }
    public static UnityEvent<int> OnEarnScore { get => _onEarnScore; }
    public static UnityEvent<int> OnUpdateScore { get => _onUpdateScore; }

    public static UnityEvent<Transform> OnExplosion { get => _onExplosion; }
}
