using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [field: SerializeField] public Transform YamsParent { get; }
    [field: SerializeField] public Transform VinesParent { get; }

    private void Awake()
    {
        if (Instance != null)
            Instance = this;

        else
        {
            Debug.LogWarning("2 Game Managers in the scene!");
            Destroy(this);
        }
    }
}