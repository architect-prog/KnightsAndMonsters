using Assets.Scripts.Utils;
using Game.Components;
using UnityEngine;
[RequireComponent(typeof(HealthComponent))]
public abstract class Unit : MonoBehaviour, IInitializable
{
    [SerializeField] protected HealthComponent _health;
  
    private void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        _health = GetComponent<HealthComponent>();
    }
}