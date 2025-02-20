using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyTurtleAnimationController : EnemyAnimController
    {
        [Space]
        [Header("Child Variables")]
        [Space]
        [Header("Animation Controllers")]
        [SerializeField] private AnimatorController _controllerIdleSpikesOut;
        [SerializeField] private AnimatorController _controllerSpikesOut;
        [SerializeField] private AnimatorController _controllerSpikesIn;
        [Space]
        [Header("Bools for Debug")]
        [SerializeField] private bool _isSpikesIn = true;
        //[SerializeField] private bool _playerIsNear = false;

        private TurtleController _turtleController;
        private bool _isControllerChanging = false;

        public override void Init(TurtleController turtleController)
        {
            base.Init(turtleController);
        }

        public override void UnInit(TurtleController turtleController)
        {
            base.UnInit(turtleController);
        }

        private void OnEnable()
        {
            ChangeAnimatorController(_controllerIdle);
        }

        private void Update()
        {
            ChangingAnimations();
        }

        private void ChangingAnimations()
        {
            if (!_isControllerChanging)
            {
                if (_isSpikesIn)
                    ChangeAnimatorController(_controllerIdle);
                else if (!_isSpikesIn)
                    ChangeAnimatorController(_controllerIdleSpikesOut);
            }

            if (_isPlayerIsNear && _isSpikesIn)
            {
                StartCoroutine(SpikesOut());
            }
            else if (!_isPlayerIsNear && !_isSpikesIn)
            {
                StartCoroutine(SpikesIn());
            }
        }

        //private void ChangePlayerDetectionStatusOLD(bool isNear)
        //{ _playerIsNear = isNear; }

        private IEnumerator SpikesOut()
        {
            _isControllerChanging = true;
            ChangeAnimatorController(_controllerSpikesOut);
            yield return new WaitForSeconds(1);
            _isSpikesIn = false;
            _isControllerChanging = false;
        }

        private IEnumerator SpikesIn()
        {
            _isControllerChanging = true;
            ChangeAnimatorController(_controllerSpikesIn);
            yield return new WaitForSeconds(1);
            _isSpikesIn = true;
            _isControllerChanging = false;
        }
    }
}