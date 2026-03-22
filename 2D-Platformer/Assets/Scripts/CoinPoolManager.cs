using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPoolManager : MonoBehaviour
{
    public static CoinPoolManager Instance { get; private set; }

    [Header("Coin Setup")]
    [SerializeField] private GameObject coinPrefab;

    [Header("Pool Size")]
    [SerializeField] private int poolSize = 10;

    private ObjectPool coinPool;

    private List<Vector3> coinStartPositions = new List<Vector3>();
    private List<GameObject> activeCoins = new List<GameObject>();

    private void Awake()
    {
        Debug.Log("CoinPoolManager Awake");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeCoins();
    }

    private void InitializeCoins()
    {
        coinStartPositions.Clear();
        activeCoins.Clear();

        GameObject[] existingCoins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in existingCoins)
        {
            coinStartPositions.Add(coin.transform.position);
            Destroy(coin);
        }

        coinPool = new ObjectPool(coinPrefab, poolSize);

        SpawnAllCoins();
    }

    private void SpawnAllCoins()
    {
        Debug.Log("Coin Spawned");
        activeCoins.Clear();

        foreach (Vector3 pos in coinStartPositions)
        {
            GameObject coin = coinPool.GetObject(pos, Quaternion.identity);
            activeCoins.Add(coin);
        }
    }

    public void CollectCoin(GameObject coin)
    {
        Debug.Log("Coin Collected");
        coinPool.ReturnObject(coin);
        activeCoins.Remove(coin);
    }

    public void ResetAllCoins()
    {
        activeCoins.Clear();
        SpawnAllCoins();
    }
}