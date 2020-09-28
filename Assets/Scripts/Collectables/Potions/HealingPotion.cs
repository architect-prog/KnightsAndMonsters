using Game.Components;
using Game.Utils;
using UnityEngine;

public class HealingPotion : MonoBehaviour, ICollactable
{
    [SerializeField] private int _hpRestoreValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //HealthComponent health = collision.GetComponent<HealthComponent>();
        //if (health != null)
        //{
        //    health.Heal(_hpRestoreValue);
        //    Collect();
        //}
        ICollector collector = collision.GetComponent<ICollector>();
        if (collector != null)
        {
            collector.Take(this);
            Collect();
        }
    }

    public void Collect()
    {

    }
}