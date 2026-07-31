using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinRespawner respawner;
    private int spawnIndex;
    private enum CoinType { Ruby, Gold, Silver }
    [SerializeField] private CoinType coinType;

    [Header ("Collection Details")]
    private bool isCollected = false;
    [SerializeField] private float lifeTime = 1f;

    [Header("Extra Details")]
    [SerializeField] private AudioClip coinSoundClip;

    public void Init(CoinRespawner respawner, int spawnIndex)
    {
        this.respawner = respawner;
        this.spawnIndex = spawnIndex;
        Invoke(nameof(SelfDestruct), lifeTime); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance != null && GameManager.instance.IsGameOver)
            return;

        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        if (SoundFXManager.Instance != null && coinSoundClip != null)
            SoundFXManager.Instance.PlaySFXClip(coinSoundClip, transform, 0.75f);

        int value = GetCoinValue();
        ScoreManager.Instance.AddScore(value);

        CancelInvoke(nameof(SelfDestruct));
        Destroy(gameObject);
    }

    private int GetCoinValue()
    {
        switch (coinType)
        {
            case CoinType.Ruby: return 10;
            case CoinType.Gold: return 5;
            case CoinType.Silver: return 2;
            default: return 1;
        }
    }

    private void SelfDestruct()
    {
        if (!isCollected)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (respawner != null)
        {
            respawner.FreeSpawnPoint(spawnIndex);
        }
    }
}
