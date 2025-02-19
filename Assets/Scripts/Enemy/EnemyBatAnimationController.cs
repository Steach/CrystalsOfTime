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

        private bool _isControllerChanging = false;

        public override void Init(BatController batController)
        {
            base.Init(batController);
        }

        public override void UnInit(BatController batController)
        {
            base.UnInit(batController);
        }

        private void Update()
        {
            ChangingAnimation();
        }

        private void ChangingAnimation()
        {
            if (!_isControllerChanging)
            {
                if (_isPlayerIsNear && _isCellingIn)
                {
                    StartCoroutine(BatIsCellingOut());
                    _isCellingIn = false;

                }
                else if (!_isPlayerIsNear && !_isCellingIn)
                {
                    StartCoroutine(BatIsCellingIn());
                    _isCellingIn = true;
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