using CrystalOfTime.Systems.Environments;
using CrystalOfTime.Systems.Environments.Portal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrystalOfTime.Systems.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerDownPoint;
        [SerializeField] private List<ManageGroundColliders> _groundsColliders;
        [SerializeField] private GameObject _portal;

        private PlayerColliding _playerColliding;

        private float _crystalsCount;

        private void Awake()
        {
            foreach (var ground in _groundsColliders)
                ground.Init(_playerDownPoint);
        }

        private void OnEnable()
        {
            _playerColliding = _player.GetComponent<PlayerColliding>();
            PlayerColliding.GrabCrystal += CheckCrystalCount;
            _playerColliding.PlayerPortalEvent += LevelComplete;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabCrystal -= CheckCrystalCount;
            _playerColliding.PlayerPortalEvent -= LevelComplete;
        }

        private void CheckCrystalCount(int crystal)
        {
            PortalAnimationController portalController;
            if (TryGetComponent<PortalAnimationController>(out portalController))
            {
                _crystalsCount = crystal;

                if (_crystalsCount >= 3)
                    portalController.InitPortal();
                else
                    portalController.ClosePortal();
            }
        }

        private void LevelComplete()
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;

            if (currentScene == 0)
                SceneManager.LoadScene(1);
            else if(currentScene == 1)
                SceneManager.LoadScene(2);
        }

        public GameObject GetPlayer()
        { return _player; }
    }
}