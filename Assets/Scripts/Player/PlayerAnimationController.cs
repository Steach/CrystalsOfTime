using CrystalOfTime.Systems.InputSystem;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrystalOfTime.Player.Animation
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInputMethod _playerInputMethod;
        [Space]
        [Header("Animation Controllers")]
        [SerializeField] private AnimatorController _idle;
        [SerializeField] private AnimatorController _run;
        [SerializeField] private AnimatorController _jump;
        [SerializeField] private AnimatorController _castingSpell;
        [SerializeField] private AnimatorController _meleeAttake;
        [SerializeField] private AnimatorController _getHit;
        [SerializeField] private AnimatorController _death;
        [SerializeField] private AnimatorController _fall;
        [Space]
        [Header("Animation Timing")]
        [SerializeField] private float _idleTime;
        [SerializeField] private float _runTime;
        [SerializeField] private float _castingTime;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _meleeAttakeTime;
        [SerializeField] private float _deathTime;
        [SerializeField] private float _getHitTime;
        [SerializeField] private float _fallTime;

        private bool _isJump = false;
        private bool _isCasting = false;
        private bool _isJumpAnimationIsFinish = true;
        private bool _isDeath = false;
        private bool _isGetHit = false;

        private void Awake()
        {
            ChangeAnimation(_idle);
        }

        private void OnEnable()
        {
            _playerInputMethod.PlayerJumpTrigger += JumpingAnimation;
            _playerInputMethod.PlayerCastingSpellTrigger += CastingSpell;
            PlayerColliding.PlayerDeath += PlayerIsDead;
        }

        private void Update()
        {
            if (!_isDeath)
            {
                if (!_isGetHit)
                {
                    if (!_isCasting)
                    {
                        if ((_playerInputMethod.MoveInput.x > 0 || _playerInputMethod.MoveInput.x < 0) && _playerInputMethod.IsGrounded && !_isJump)
                            ChangeAnimation(_run);
                        else if (_playerInputMethod.MoveInput.x == 0 && _playerInputMethod.IsGrounded && !_isJump)
                            ChangeAnimation(_idle);


                        if (_isJumpAnimationIsFinish && _isJump && _playerInputMethod.IsGrounded)
                        {
                            _isJump = false;
                        }
                        else if (_isJumpAnimationIsFinish && _isJump && !_playerInputMethod.IsGrounded)
                        {
                            ChangeAnimation(_fall);
                        }
                        else if (!_isJump && !_playerInputMethod.IsGrounded)
                        {
                            ChangeAnimation(_fall);
                        }
                    }
                    else if (_isCasting)
                    {
                        ChangeAnimation(_castingSpell);
                    }
                }
                else if (_isGetHit)
                {
                    ChangeAnimation(_getHit);
                }
            }
            else if (_isDeath)
            {
                ChangeAnimation(_death);
            }
        }

        private void OnDestroy()
        {
            _playerInputMethod.PlayerJumpTrigger -= JumpingAnimation;
            _playerInputMethod.PlayerCastingSpellTrigger -= CastingSpell;
            PlayerColliding.PlayerDeath -= PlayerIsDead;
        }

        private void CastingSpell(bool isCasting)
        {
            _isCasting = true;
            ChangeAnimation(_castingSpell);
            StartCoroutine(RestoreAnimationParametrs(_castingTime, PlayerAnimationControllersEnum.Casting));
        }

        private void PlayerIsDead(bool isDead)
        {
            _isDeath = true;
        }

        private void ChangeAnimation(AnimatorController newAnimatorController)
        {
            _animator.runtimeAnimatorController = newAnimatorController;
        }

        private void JumpingAnimation()
        {
            StartCoroutine(Jump());
        }

        private IEnumerator RestoreAnimationParametrs(float animationTimer, PlayerAnimationControllersEnum controllerID)
        {
            yield return new WaitForSeconds(animationTimer);

            switch (controllerID)
            {
                case PlayerAnimationControllersEnum.Idle:
                    break;
                case PlayerAnimationControllersEnum.Run:
                    break;
                case PlayerAnimationControllersEnum.Jump:
                    break;
                case PlayerAnimationControllersEnum.Fall:
                    break;
                case PlayerAnimationControllersEnum.GetHit:
                    _isGetHit = false;
                    break;
                case PlayerAnimationControllersEnum.Casting:
                    _isCasting = false;
                    break;
                case PlayerAnimationControllersEnum.Death:
                    break;
            }
        }

        private IEnumerator Jump()
        {
            _isJump = true;
            _isJumpAnimationIsFinish = false;
            ChangeAnimation(_jump);
            yield return new WaitForSeconds(_jumpTime);
            _isJumpAnimationIsFinish = true;

            if (_playerInputMethod.IsGrounded == true)
            {
                _isJump = false;
                ChangeAnimation(_idle);
            }   
        }
    }
}