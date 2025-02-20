using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animatorController;
        [SerializeField] private AnimatorController _controllerDefault;
        [SerializeField] private AnimatorController _controllerDamaged;
        [SerializeField] private AnimatorController _ControllerDeath;

        private void OnEnable()
        {
            ChangeAnimatorController(_controllerDefault);
        }

        public void Init(EnemyController enemyController)
        {
            enemyController.EnemyGetHitTrigger += GetHit;
            enemyController.EnemyDeathTrigger += Death;
        }

        public void UnInit(EnemyController enemyController)
        {
            enemyController.EnemyGetHitTrigger -= GetHit;
            enemyController.EnemyDeathTrigger -= Death;
        }

        public void GetHit()
        {
            StartCoroutine(Damaged());
        }

        public void Death()
        {
            StartCoroutine(GhostDeath());
        }

        private IEnumerator Damaged()
        {
            ChangeAnimatorController(_controllerDamaged);
            yield return new WaitForSeconds(0.3f);
            ChangeAnimatorController(_controllerDefault);
        }

        private IEnumerator GhostDeath()
        {
            ChangeAnimatorController(_ControllerDeath);
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }

        private void ChangeAnimatorController(AnimatorController controller)
        {
            _animatorController.runtimeAnimatorController = controller;
        }
    }
}