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

        private float _crystalsCount;

        private void Awake()
        {
            foreach (var ground in _groundsColliders)
                ground.Init(_playerDownPoint);
        }

        private void OnEnable()
        {
            PlayerColliding.GrabCrystal += CheckCrystalCount;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabCrystal -= CheckCrystalCount;
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

        public GameObject GetPlayer()
        { return _player; }
    }
}