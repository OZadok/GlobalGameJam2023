using SuperMaxim.Messaging;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    private readonly TickEvent _tickEvent = new TickEvent();
    private float _nextTickTime;
    [SerializeField] [Range(1, 30)] private int _tickPerSecond = 12;
    private float TimeBetweenTicks => 1f / _tickPerSecond;

    private void Awake()
    {
        _nextTickTime = GetTime() + TimeBetweenTicks;
    }

    private void Update()
    {
        // todo - improve by add number of ticks to the tickEvent, or calculate how much ticks instead of using while loop
        var currentTime = GetTime();
        while (_nextTickTime <= currentTime)
        {
            _nextTickTime += TimeBetweenTicks;
            Tick();
        }
    }

    private void Tick()
    {
        Messenger.Default.Publish(_tickEvent);
    }

    private float GetTime()
    {
        return Time.time;
    }
}