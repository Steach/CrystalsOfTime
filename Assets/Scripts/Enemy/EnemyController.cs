using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _hp;
        [SerializeField] private EnemyAnimationController _enemyAnimatorController;
        [SerializeField] private EnemyMovement _enemyMovement;

        public delegate void EnemyGetHitTriggerHandler();
        public event EnemyGetHitTriggerHandler EnemyGetHitTrigger;

        public delegate void EnemyDeathTriggerHandler();
        public event EnemyDeathTriggerHandler EnemyDeathTrigger;

        private void Start()
        {
            _enemyAnimatorController.Init(this);
        }

        private void OnDestroy()
        {
            _enemyAnimatorController.UnInit(this);
        }

        public void GetHit(float damage)
        {
            _hp = _hp - damage;

            if(_hp > 0)
                EnemyGetHitTrigger?.Invoke();
            else if (_hp <= 0)
                EnemyDeathTrigger?.Invoke();
        }
    }
}