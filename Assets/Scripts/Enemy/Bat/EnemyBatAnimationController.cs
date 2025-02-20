using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyBatAnimationController : EnemyAnimController
    {
        [Space]
        [Header("Child variables")]
        [Space]
        [SerializeField] private AnimatorController _controllerCellingIn;
        [SerializeField] private AnimatorController _controllerCellingOut;
        [Space]
        [Header("Bools for debug")]
        [SerializeField] private bool _isCellingIn;

        private EnemyMovement _enemyMovement;

        private bool _isControllerChanging = false;

        public override void Init(BatController batController, EnemyMovement enemyMovement)
        {
            base.Init(batController, enemyMovement);
            _enemyMovement = enemyMovement;
        }

        public override void UnInit(BatController batController, EnemyMovement enemyMovement)
        {
            base.UnInit(batController, enemyMovement);
        }

        private void Update()
        {
            ChangingAnimation();
        }

        private void ChangingAnimation()
        {
            _isCellingIn = _enemyMovement.BatIsCelling;

            if (!_isControllerChanging)
            {
                if (_isPlayerIsNear && _isCellingIn)
                {
                    StartCoroutine(BatIsCellingOut());

                }
                else if (!_isPlayerIsNear && _isCellingIn)
                {
                    StartCoroutine(BatIsCellingIn());
                }
                else if (!_isPlayerIsNear && !_isCellingIn)
                {
                    ChangeAnimatorController(_controllerMove);
                }
                else if (_isPlayerIsNear && !_isCellingIn)
                {
                    ChangeAnimatorController(_controllerMove);
                }
            }
        }

        private IEnumerator BatIsCellingOut()
        {
            SwitchTheChangingAnimationStatus();
            ChangeAnimatorController(_controllerCellingOut);
            yield return new WaitForSeconds(1);
            ChangeAnimatorController(_controllerMove);
            SwitchTheChangingAnimationStatus();
        }

        private IEnumerator BatIsCellingIn()
        {
            SwitchTheChangingAnimationStatus();
            ChangeAnimatorController(_controllerCellingIn);
            yield return new WaitForSeconds(1);
            ChangeAnimatorController(_controllerIdle);
            SwitchTheChangingAnimationStatus();
        }

        private void SwitchTheChangingAnimationStatus()
        {
            _isControllerChanging = !_isControllerChanging;
        }
    }
}