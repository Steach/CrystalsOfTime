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
        public abstract void Init(TurtleController turtleController);
        public abstract void UnInit(TurtleController turtleController);

        protected virtual void ChangeAnimatorController(AnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }
    }
}