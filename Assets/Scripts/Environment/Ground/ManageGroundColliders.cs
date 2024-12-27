using UnityEngine;

namespace CrystalOfTime.Systems.Environments
{
    public class ManageGroundColliders : MonoBehaviour
    {
        [SerializeField] private Collider2D _groundCollider;
        [SerializeField] private Transform _groundDownPoint;

        private Transform _playerDownPointTransform;
        private bool _isInited = false;

        public void Init(Transform playerTransform)
        {
            if (!_isInited)
            {
                _playerDownPointTransform = playerTransform;
                _isInited = true;
            }
        }

        private void Update()
        {
            if (_playerDownPointTransform.position.y < _groundDownPoint.position.y)
                _groundCollider.enabled = false;
            if (_playerDownPointTransform.position.y > _groundDownPoint.position.y)
                _groundCollider.enabled = true;
        }
    }
}