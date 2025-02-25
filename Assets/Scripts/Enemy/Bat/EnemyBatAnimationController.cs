using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyBatAnimationController : EnemyAnimController
    {
        public override event BatIsCellingInHandler BatIsCellingInTrigger;

        [Space]
        [Header("Child variables")]
        [Space]
        [SerializeField] private AnimatorController _controllerCellingIn;
        [SerializeField] private AnimatorController _controllerCellingOut;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [Space]
        [Header("Bools for debug")]
        [SerializeField] private bool _isInStartPoint;
        [SerializeField] private BatState _currentBatState;
        [SerializeField] private BatState _prevBatState;

        private EnemyMovement _enemyMovement;

        [SerializeField] private bool _isControllerChanging = false;

        public override void Init(BatController batController, EnemyMovement enemyMovement)
        {
            base.Init(batController, enemyMovement);
            _enemyMovement = enemyMovement;
        }

        public override void UnInit(BatController batController, EnemyMovement enemyMovement)
        {
            base.UnInit(batController, enemyMovement);
        }

        private void Start()
        {
            _prevBatState = _currentBatState = BatState.InCelling;
        }

        private void Update()
        {
            ChangingAnimation();
        }

        private void ChangingAnimation()
        {
            _isInStartPoint = _enemyMovement.BatIsInStartPoint;

            if (!_isControllerChanging)
            {
                if (_prevBatState == BatState.InCelling)
                {
                    if (_isPlayerIsNear && _isInStartPoint)
                    {
                        _currentBatState = BatState.CellingOut;
                        StartCoroutine(BatIsCellingOut());
                    }
                    else if (!_isPlayerIsNear && !_isInStartPoint)
                    {
                        BatIsCellingInTrigger?.Invoke(true);
                        _currentBatState = BatState.ReturnToCelling;
                    }
                }
                else if (_prevBatState == BatState.CellingOut)
                {
                    if (_isPlayerIsNear && !_isInStartPoint)
                        _currentBatState = BatState.FollowPlayer;
                }
                else if (_prevBatState == BatState.FollowPlayer)
                {
                    if (!_isPlayerIsNear && !_isInStartPoint)
                        _currentBatState = BatState.ReturnToCelling;
                }
                else if (_prevBatState == BatState.ReturnToCelling)
                {
                    if (!_isPlayerIsNear && _isInStartPoint)
                    {
                        _currentBatState = BatState.CellingIn;
                        StartCoroutine(BatIsCellingIn());
                    } 
                }
                else if (_prevBatState == BatState.CellingIn)
                {
                    if (!_isPlayerIsNear && _isInStartPoint)
                        _currentBatState = BatState.InCelling;
                }

                switch (_currentBatState)
                {
                    case BatState.InCelling:
                        ChangeAnimatorController(_controllerIdle);
                        break;
                    case BatState.ReturnToCelling:
                        ChangeAnimatorController(_controllerMove);
                        break;
                }
            }

            _prevBatState = _currentBatState;
        }

        private IEnumerator BatIsCellingOut()
        {
            _isControllerChanging = true;
            _currentBatState = BatState.CellingOut;
            ChangeAnimatorController(_controllerCellingOut);
            yield return new WaitForSeconds(1);
            BatIsCellingInTrigger?.Invoke(true);
            _currentBatState = BatState.FollowPlayer;
            ChangeAnimatorController(_controllerMove);
            _isControllerChanging = false;
        }

        private IEnumerator BatIsCellingIn()
        {
            _isControllerChanging = true;
            _currentBatState = BatState.CellingIn;
            ChangeAnimatorController(_controllerCellingIn);
            yield return new WaitForSeconds(1);
            BatIsCellingInTrigger?.Invoke(false);
            _currentBatState = BatState.InCelling;
            ChangeAnimatorController(_controllerIdle);
            _isControllerChanging = false;
        }

        public enum BatState
        {
            InCelling = 0,
            FollowPlayer = 1,
            ReturnToCelling = 2,
            CellingIn = 3,
            CellingOut = 4
        }
    }
}