using CrystalOfTime.Systems.InputSystem;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

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
        private bool _jumpAnimationIsFinish = true;

        private void Awake()
        {
            ChangeAnimation(_idle);
        }

        private void OnEnable()
        {
            _playerInputMethod.PlayerJumpTrigger += JumpingAnimation;
        }

        private void Update()
        {
            if ((_playerInputMethod.MoveInput.x > 0 || _playerInputMethod.MoveInput.x < 0) && _playerInputMethod.IsGrounded && !_isJump)
                ChangeAnimation(_run);
            else if (_playerInputMethod.MoveInput.x == 0 && _playerInputMethod.IsGrounded && !_isJump)
                ChangeAnimation(_idle);


            if (_jumpAnimationIsFinish && _isJump && _playerInputMethod.IsGrounded)
            {
                _isJump = false;
            }
            else if (_jumpAnimationIsFinish && _isJump && !_playerInputMethod.IsGrounded)
            {
                ChangeAnimation(_fall);
            }
        }

        private void OnDestroy()
        {
            _playerInputMethod.PlayerJumpTrigger -= JumpingAnimation;
        }

        private void ChangeAnimation(AnimatorController newAnimatorController)
        {
            _animator.runtimeAnimatorController = newAnimatorController;
        }

        private void JumpingAnimation()
        {
            StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            _isJump = true;
            _jumpAnimationIsFinish = false;
            ChangeAnimation(_jump);
            yield return new WaitForSeconds(_jumpTime);
            _jumpAnimationIsFinish = true;

            if (_playerInputMethod.IsGrounded == true)
            {
                _isJump = false;
                ChangeAnimation(_idle);
            }   
        }
    }
}