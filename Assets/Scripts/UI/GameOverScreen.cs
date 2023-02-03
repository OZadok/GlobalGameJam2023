using Events;
using SuperMaxim.Messaging;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent gameOverEvent)
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }
}