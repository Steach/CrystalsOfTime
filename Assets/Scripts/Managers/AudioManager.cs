using CrystalOfTime.Systems.Managers;
using UnityEngine;

namespace CrystalOfTime.Systems.SFX
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _backGroundAudioClip;

        private void OnEnable()
        {
            _audioSource.loop = true;
            _audioSource.clip = _backGroundAudioClip;
            _audioSource.Play();
        }
    }
}