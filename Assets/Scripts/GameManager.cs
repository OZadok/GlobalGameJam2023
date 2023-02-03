using UnityEngine;
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
}