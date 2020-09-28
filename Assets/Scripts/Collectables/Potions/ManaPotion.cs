using Assets.Scripts.Components;
using Game.Components.AbstractComponents;
using Game.Utils;
using UnityEngine;

public class ManaPotion : MonoBehaviour, ICollactable, IPotion
{
    [SerializeField] private int _manaRestoreValue;   

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
        
    }

    public void Use(GameComponent component)
    {
        
    }
}
