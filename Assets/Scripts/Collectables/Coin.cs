using Game.Utils;
using UnityEngine;

namespace Game.Collectables
{
    public class Coin : MonoBehaviour, ICollactable
    {
        [SerializeField] private int _value;
        [SerializeField] private ParticleSystem _collectEffect;      
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollector collector = collision.GetComponent<ICollector>();
            if (collector != null)
            {
                collector.Take(this);
                Collect();               
            }
        }

        public void Collect()
        {
            //particleSystem.Play();
            //player.Coins += money;
            //player.CoinsBar.UpdateCoins(1);
            //Destroy(GetComponentInChildren<SpriteRenderer>());
            //Destroy(gameObject, 0.4f);
        }
    }
}


