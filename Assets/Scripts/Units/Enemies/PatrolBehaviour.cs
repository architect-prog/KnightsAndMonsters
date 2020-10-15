using Game.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Units.Enemies
{
    public class PatrolBehaviour : StateMachineBehaviour
    {
        private EnemyVisionComponent _visionComponent;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _visionComponent = animator.GetComponent<EnemyVisionComponent>();

        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}

