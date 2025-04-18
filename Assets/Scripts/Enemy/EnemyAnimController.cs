using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public abstract class EnemyAnimController : MonoBehaviour
    {
        public delegate void BatIsCellingInHandler(bool isCellingIn);
        public virtual event BatIsCellingInHandler BatIsCellingInTrigger;

        [Header("Parent Variables")]
        [Space]
        [SerializeField] protected Animator _animator;
        [SerializeField] protected AnimatorController _controllerIdle;
        [SerializeField] protected AnimatorController _controllerGetHit;
        [SerializeField] protected AnimatorController _controllerMove;
        [SerializeField] protected AnimatorController _controllerDeath;

        [SerializeField] protected bool _isPlayerIsNear;

        //public abstract void Init();
        //public virtual void Init(){ }
        //public virtual void UnInit(){ }

        public virtual void Init(TurtleController turtleController)
        {
            turtleController.EnemyPlayerDetectionTrigger += ChangePlayerDetectionStatus;
        }
        public virtual void UnInit(TurtleController turtleController)
        {
            turtleController.EnemyPlayerDetectionTrigger -= ChangePlayerDetectionStatus;
        }

        public virtual void Init(BatController batController, EnemyMovement enemyMovement)
        {
            batController.EnemyPlayerDetectionTrigger += ChangePlayerDetectionStatus;
        }
        public virtual void UnInit(BatController batController, EnemyMovement enemyMovement)
        {
            batController.EnemyPlayerDetectionTrigger -= ChangePlayerDetectionStatus;
        }

        protected void ChangePlayerDetectionStatus(bool isNear)
        {
            _isPlayerIsNear = isNear;
        }

        protected virtual void ChangeAnimatorController(AnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }
    }
}