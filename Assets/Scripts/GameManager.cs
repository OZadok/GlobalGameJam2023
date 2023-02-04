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

    [SerializeField] private float _gameTime = 60f;
    private float _gameStartTime;

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
    }

    private void Start()
    {
        _gameStartTime = GetCurrentTime();
    }

    private void Update()
    {
        if (GetRemainingTime() <= 0)
        {
            Messenger.Default.Publish(new GameOverEvent());
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public float GetRemainingTime()
    {
        return _gameTime - (GetCurrentTime() - _gameStartTime);
    }

    private float GetCurrentTime()
    {
        return Time.time;
    }
}