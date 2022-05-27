using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private GameObject coinPickupParticle;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Coin"))
        {
            CurrencyManager.Instance.CollectCoin();
            var particle = Instantiate(coinPickupParticle, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(particle, 2f);
            SoundHandler.Instance.PlayAudio(SoundEffect.CoinPickUp);
        }
    }
}
