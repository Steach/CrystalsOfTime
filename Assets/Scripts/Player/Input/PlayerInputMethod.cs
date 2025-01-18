using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrystalOfTime.Systems.InputSystem
{
    public class PlayerInputMethod : MonoBehaviour
    {
        public delegate void PlayerCastingSpellTriggerHandler(bool isFlipping);
        public event PlayerCastingSpellTriggerHandler PlayerCastingSpellTrigger;

        public delegate void PlayerJumpTriggerHandler();
        public event PlayerJumpTriggerHandler PlayerJumpTrigger;

        [Header("Player Move Settings")]
        [SerializeField] private Rigidbody2D _playerRigidbody2d;
        [SerializeField] private float _moveSpeed;
        [Space]
        [Header("Jump settings")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _layerMask;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private PlayerController _playerController;

        private bool _isDead = false;

        public bool IsCasting { get; private set; }
        public Vector2 MoveInput { get; private set; }
        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _playerController = new PlayerController();
            _playerController.Enable();
            IsGrounded = false;
        }

        private void OnEnable()
        {
            _playerController.Movement.Move.performed += PlayerMove;
            _playerController.Movement.Move.canceled += PlayerStopMove;
            _playerController.Movement.Jump.performed += Jump;
            _playerController.Attake.CastSpell.performed += CastingSpell;
            PlayerColliding.PlayerDeath += CheckPlayerDeath;
        }

        private void OnDisable()
        {
            _playerController.Movement.Move.performed -= PlayerMove;
            _playerController.Movement.Move.canceled -= PlayerStopMove;
            _playerController.Movement.Jump.performed -= Jump;
            _playerController.Attake.CastSpell.performed -= CastingSpell;
            PlayerColliding.PlayerDeath -= CheckPlayerDeath;
            _playerController.Disable();
        }

        public void PlayerMove(InputAction.CallbackContext context)
        { 
            MoveInput = context.ReadValue<Vector2>();
        }

        public void PlayerStopMove(InputAction.CallbackContext context)
        {
            MoveInput = new Vector2(0, 0);
        }

        public void Jump(InputAction.CallbackContext context)
        {
            if(IsGrounded && !IsCasting)
            {
                _playerRigidbody2d.AddForce(Vector2.up * _jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                PlayerJumpTrigger?.Invoke();
            }  
        }

        private void CastingSpell(InputAction.CallbackContext context)
        {
            if (IsGrounded && !IsCasting)
            {
                IsCasting = true;

                if (MoveInput.x > 0)
                    PlayerCastingSpellTrigger?.Invoke(false);
                else if (MoveInput.x < 0)
                    PlayerCastingSpellTrigger?.Invoke(true);
                else if (MoveInput.x == 0)
                    PlayerCastingSpellTrigger?.Invoke(_spriteRenderer.flipX);

                StartCoroutine(ResetIsCasting());
            }
        }

        private void Update()
        {
            if (!_isDead && !IsCasting)
            {
                CheckPlayerIsGrounded();
                Vector3 movement = new Vector3(MoveInput.x, 0, 0);
                transform.position += movement * _moveSpeed * Time.deltaTime;
            }
            else if (_isDead)
            {
                Debug.Log("PLAYER IS DEAD GAME OVER");

                _playerController.Movement.Move.performed -= PlayerMove;
                _playerController.Movement.Move.canceled -= PlayerStopMove;
                _playerController.Movement.Jump.performed -= Jump;
                PlayerColliding.PlayerDeath -= CheckPlayerDeath;
                _playerController.Disable();
            }
        }

        private void CheckPlayerIsGrounded()
        {
            Vector2 origin = transform.position;
            Vector2 direction = Vector3.down;

            IsGrounded = Physics2D.Raycast(origin, direction, _rayDistance, _layerMask);
        }

        private void CheckPlayerDeath(bool isDead)
        {
            _isDead = isDead;
        }

        private IEnumerator ResetIsCasting()
        {
            yield return new WaitForSeconds(0.7f);
            IsCasting = false;
        }
    }
}