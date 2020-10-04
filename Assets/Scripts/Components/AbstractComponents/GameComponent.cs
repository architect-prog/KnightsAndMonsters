using Assets.Scripts.Utils;
using UnityEngine;

namespace Game.Components.AbstractComponents
{
    public abstract class GameComponent : MonoBehaviour, IInitializable
    {
        private void Start()
        {
            Initialize();  
        }
        public virtual void Initialize() { }
    }
}