using System.Collections;
using UnityEngine;

public class CoinThrower : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnPoint;
    [SerializeField] private float coinLifetime;
    [SerializeField] private float coinAddForceMultiplier;
    [SerializeField] private float coinlinearVeloityMultiplier;

    [Space]
    [SerializeField] private MonoBehaviour coroutineTarget;

    private Vector3 linearVelocity;

    public void OnLinearVelocityChanged(Vector3 velocity)
    {
        linearVelocity = velocity;
    }

    public void OnAdditionalAction()
    {
        var coin = Instantiate(coinPrefab, coinSpawnPoint.position, coinSpawnPoint.rotation);

        var coinRb = coin.GetComponent<Rigidbody>();
        coinRb.linearVelocity = linearVelocity * coinlinearVeloityMultiplier;
        coinRb.AddForce((-transform.forward + transform.up) * coinAddForceMultiplier, ForceMode.Acceleration);

        coroutineTarget.StartCoroutine(DestroyCoinAfter(coin, coinLifetime));
    }

    private IEnumerator DestroyCoinAfter(GameObject coin, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(coin);
    }
}
