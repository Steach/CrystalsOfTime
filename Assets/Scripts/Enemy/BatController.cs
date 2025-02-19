using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class BatController : EnemiesController
    {
        private void OnEnable()
        {
            _enemyAnimationController.Init(this);
            _enemyMovement.Init(this);
        }

        private void OnDisable()
        {
            _enemyAnimationController.UnInit(this);
            _enemyMovement.UnInit(this);
        }

        private void Update()
        {
            CheckPlayerDistanceOnCircle();
        }
    }
}