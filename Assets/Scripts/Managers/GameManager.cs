using CrystalOfTime.Systems.Environments;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalOfTime.Systems.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerDownPoint;
        [SerializeField] private List<ManageGroundColliders> _groundsColliders;

        private void Awake()
        {
            foreach (var ground in _groundsColliders)
                ground.Init(_playerDownPoint);
        }

        public GameObject GetPlayer()
        { return _player; }
    }
}

