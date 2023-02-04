using System;
using System.Collections;
using System.Collections.Generic;
using SuperMaxim.Messaging;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void OnEnable()
    {
        Messenger.Default.Subscribe<GameStartEvent>(OnGameStart);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<GameStartEvent>(OnGameStart);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _canvasGroup.gameObject.SetActive(false);
    }
}
