using CrystalOfTime.Systems.InputSystem;
using UnityEngine;

namespace CrystalOfTime.Player.SFX
{
    public class PlayerFSXController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Space]
        [Header("Sounds")]
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _runSound;
        [SerializeField] private AudioClip _getHitSound;
        [SerializeField] private AudioClip _castingSpellSound;
        [SerializeField] private AudioClip _deathSound;

        private PlayerInputMethod _playerInputMethod;
        private PlayerColliding _playerColliding;

        private void Awake()
        {
            _playerInputMethod = gameObject.GetComponent<PlayerInputMethod>();
            _playerColliding = gameObject.GetComponent<PlayerColliding>();
            _playerInputMethod.PlayerJumpTrigger += PlayJumpSFX;
            _playerInputMethod.PlayerCastingSpellTrigger += PlayerCastingSpellSFX;
            _playerColliding.PlayerDamaged += GetHitSFX;
        }

        private void OnDestroy()
        {
            _playerInputMethod.PlayerJumpTrigger -= PlayJumpSFX;
            _playerInputMethod.PlayerCastingSpellTrigger -= PlayerCastingSpellSFX;
            _playerColliding.PlayerDamaged -= GetHitSFX;
        }

        private void Update()
        {
            if (_playerInputMethod.IsGrounded && _playerInputMethod.MoveInput.x != 0 && !_audioSource.isPlaying && !_playerInputMethod.IsCasting)
            {
                _audioSource.clip = _runSound;
                _audioSource.Play();
            }
            else if (_playerInputMethod.IsGrounded && _playerInputMethod.MoveInput.x == 0 && _audioSource.isPlaying)
            {
                _audioSource.clip = null;
            }
            else if (!_playerInputMethod.IsGrounded && _playerInputMethod.MoveInput.x != 0 && _audioSource.isPlaying)
            {
                _audioSource.clip = null;
            }
        }

        private void PlayJumpSFX()
        {
            _audioSource.PlayOneShot(_jumpSound);
        }

        private void PlayerCastingSpellSFX(bool isCasting)
        {
            _audioSource.PlayOneShot(_castingSpellSound);
        }

        private void GetHitSFX(float damage)
        {
            _audioSource.PlayOneShot(_getHitSound);
        }
    }
}