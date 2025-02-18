using CrystalOfTime.Systems.Managers;
using UnityEngine;

namespace CrystalOfTime.Systems.SFX
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
        [Header("Background FSX")]
        [SerializeField] private AudioSource _ambiendAudioSource;
        [SerializeField] private AudioClip _backGroundAudioClip;
        [Space]
        [Header("Collactable SFX")]
        [SerializeField] private AudioSource _collactableSFXAudioSource;
        [SerializeField] private AudioClip _healthPotionSFX;
        [SerializeField] private AudioClip _crystalGrabSFX;

        private PlayerColliding _playerColliding;

        private void OnEnable()
        {
            _ambiendAudioSource.loop = true;
            _ambiendAudioSource.clip = _backGroundAudioClip;
            _ambiendAudioSource.Play();

            PlayerColliding.GrabCrystal += PlayCrystalGrabSFX;
            PlayerColliding.GrabHeath += PlayHealthPotionSFX;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabCrystal -= PlayCrystalGrabSFX;
            PlayerColliding.GrabHeath -= PlayHealthPotionSFX;
        }

        private void PlayHealthPotionSFX()
        {
            _collactableSFXAudioSource.PlayOneShot(_healthPotionSFX);
        }

        private void PlayCrystalGrabSFX(int crystal)
        {
            if(crystal > 0)
                _collactableSFXAudioSource.PlayOneShot(_crystalGrabSFX);
        }
    }
}