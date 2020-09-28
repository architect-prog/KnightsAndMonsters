using Assets.Scripts.Components;
using Game.Components;
using Game.Components.AbstractComponents;
using Game.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(DamageComponent), typeof(ResistComponent), typeof(CharacterMovingComponent))]
    [RequireComponent(typeof(ManaComponent))]
    public class Character : Unit, ICollector
    {
        [SerializeField] private ManaComponent _mana;
        [SerializeField] private DamageComponent _damage;
        [SerializeField] private ResistComponent _resist;
        [SerializeField] private CharacterMovingComponent _movingComponent;

        public override void Initialize()
        {
            base.Initialize();
            _mana = GetComponent<ManaComponent>();
            _damage = GetComponent<DamageComponent>();
            _resist = GetComponent<ResistComponent>();
            _movingComponent = GetComponent<CharacterMovingComponent>();
        }

        public void Take(ICollactable collactable)
        {
            
        }
    }
}

