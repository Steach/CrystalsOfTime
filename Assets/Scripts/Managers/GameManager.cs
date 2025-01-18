using CrystalOfTime.Systems.Environments;
using CrystalOfTime.Systems.Environments.Portal;
using System.Collections.Generic;
using UnityEngine;

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
            _playerColliding.PlayerPortalEevent += LevelComplete;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabCrystal -= CheckCrystalCount;
            _playerColliding.PlayerPortalEevent -= LevelComplete;
        }

        private void CheckCrystalCount(int crystal)
        {
            _crystalsCount = crystal;
            if (_crystalsCount >= 3)
            {
                var portalController = _portal.GetComponent<PortalAnimationController>();
                portalController.InitPortal();
            }
            else
            {
                var portalController = _portal.GetComponent<PortalAnimationController>();
                portalController.ClosePortal();
            }
        }

        private void LevelComplete()
        {
            Debug.Log($"Level is complete! Congratulation!");
        }

        public GameObject GetPlayer()
        { return _player; }
    }
}