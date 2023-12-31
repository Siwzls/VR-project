using TMPro;
using UnityEngine;

namespace EnemyAI 
{
    public class ChasePlayerBehaviour : EnemyBehaviour
    {
        private const string ANIMATION_NAME = "Walking";

        public ChasePlayerBehaviour(EnemyBehaviourHandler enemyReference) : base(enemyReference) 
        {
            _animationName = ANIMATION_NAME;
            _canBeUpdated = true;
        }

        public override void Enter()
        {
            _enemyReference.WarningIcon.SetActive(true);
        }

        public override void Update()
        {
            _enemyReference.EnemyMover.Agent.SetDestination(_enemyReference.PlayerTransform.position);
        }

        public override void Exit()
        {
            _enemyReference.WarningIcon.SetActive(false);
        }
    }
}