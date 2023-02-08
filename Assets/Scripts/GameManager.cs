using System;
using Events;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yams;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] public Transform yamsParent;
    [SerializeField] public Transform vinesParent;
    [SerializeField] private Vine vinePrefab;
    [SerializeField] public YamStateManager yamPrefab;

    [SerializeField] public Collider GardenBedCollider;

    public float GameTime { get; } = 60f;
    private float _gameStartTime;

    private bool _isGameStarted;
    private bool _isGameEnded;

    [SerializeField] public Transform PlayerTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
        {
            Debug.LogWarning("2 Game Managers in the scene!");
            Destroy(this);
        }
        
        Time.timeScale = 0;
    }

    private void Start()
    {
        _gameStartTime = GetCurrentTime();
    }

    private void Update()
    {
        if (!_isGameEnded && GetRemainingTime() <= 0)
        {
            _isGameEnded = true;
            Messenger.Default.Publish(new GameOverEvent());
        }

        if (Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }

        if (!_isGameStarted && Input.anyKey)
        {
            _isGameStarted = true;
            StartGame();
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public float GetRemainingTime()
    {
        return GameTime - (GetCurrentTime() - _gameStartTime);
    }

    private float GetCurrentTime()
    {
        return Time.time;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Messenger.Default.Publish(new GameStartEvent());
    }
}