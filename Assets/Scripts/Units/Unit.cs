using Assets.Scripts.Utils;
using Game.Components;
using UnityEngine;
[RequireComponent(typeof(HealthComponent))]
public abstract class Unit : MonoBehaviour, IInitializable
{
    protected HealthComponent _health;
    public HealthComponent Health { get => _health; }

    public abstract void ApplyDamage(DamageComponent damage);

    private void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        _health = GetComponent<HealthComponent>();
    }
}