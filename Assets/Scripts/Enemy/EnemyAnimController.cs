using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public abstract class EnemyAnimController : MonoBehaviour
    {
        [Header("Parent Variables")]
        [Space]
        [SerializeField] protected Animator _animator;
        [SerializeField] protected AnimatorController _controllerIdle;
        [SerializeField] protected AnimatorController _controllerGetHit;
        [SerializeField] protected AnimatorController _controllerMove;
        [SerializeField] protected AnimatorController _controllerDeath;

        //public abstract void Init();
        public virtual void Init()
        { }
        public virtual void UnInit()
        { }

        public virtual void Init(TurtleController turtleController)
        { }
        public virtual void UnInit(TurtleController turtleController)
        { }

        public virtual void Init(EnemyBatAnimationController batController)
        { }
        public virtual void UnInit(EnemyBatAnimationController batController)
        { }

        protected virtual void ChangeAnimatorController(AnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }
    }
}