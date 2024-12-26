using UnityEngine;
using UnityEngine.InputSystem;

namespace CrystalOfTime.Systems.InputSystem
{
    public class PlayerInputMethod : MonoBehaviour
    {
        private PlayerController _playerController;

        [SerializeField] private Rigidbody2D _playerRigidbody2d;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;

        public Vector2 MoveInput { get; private set; }

        private void Awake()
        {
            _playerController = new PlayerController();
            _playerController.Enable();

            _playerController.Movement.Move.performed += PlayerMove;
            _playerController.Movement.Move.canceled += PlayerStopMove;
            _playerController.Movement.Jump.performed += Jump;
        }

        public void PlayerMove(InputAction.CallbackContext context)
        { 
            MoveInput = context.ReadValue<Vector2>();
            Debug.Log($"{MoveInput}");
        }

        public void PlayerStopMove(InputAction.CallbackContext context)
        {
            MoveInput = new Vector2(0, 0);
        }

        public void Jump(InputAction.CallbackContext context)
        {
            _playerRigidbody2d.AddForce(Vector2.up * _jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        private void Update()
        {
            Vector3 movement = new Vector3(MoveInput.x, 0, 0);
            transform.position += movement * _moveSpeed * Time.deltaTime;
        }
    }
}