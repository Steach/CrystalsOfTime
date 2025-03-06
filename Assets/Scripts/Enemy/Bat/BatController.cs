using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class BatController : EnemiesController
    {
        private void OnEnable()
        {
            _enemyAnimationController.Init(this, _enemyMovement);
            _enemyMovement.Init(this);
            _enemyAnimationController.BatIsCellingInTrigger += ChangeMovementStatus;
            _enemySpellCaster.Init(_enemyMovement);
        }

        private void OnDisable()
        {
            _enemyAnimationController.UnInit(this, _enemyMovement);
            _enemyMovement.UnInit(this);
            _enemyAnimationController.BatIsCellingInTrigger -= ChangeMovementStatus;
            _enemySpellCaster.UnInit(_enemyMovement);
        }

        private void Update()
        {
            CheckPlayerDistanceOnCircle();
        }

        private void ChangeMovementStatus(bool canMove)
        {
            _enemyMovement.ChangeIsCellingInStatus(canMove);
        }
    }
}