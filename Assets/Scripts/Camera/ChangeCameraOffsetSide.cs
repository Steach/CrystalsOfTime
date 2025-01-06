using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;
using CrystalOfTime.Systems.InputSystem;

namespace CrystalOfTime.Systems.Camera
{
    public class ChangeCameraOffsetSide : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _vcamera;
        [SerializeField] private Transform _playerTransform;

        [SerializeField] private float _xOffset;
        [SerializeField] private float _defaultOffsetX;
        [SerializeField] private float _smoothTime;

        [SerializeField] private PlayerInputMethod _playerInputMethod;

        private CinemachineTransposer _cmTransposer;
        private Vector3 _velocity = Vector3.zero;

        private void Awake()
        {
            _cmTransposer = _vcamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Update()
        {
            float playerDirection = _playerInputMethod.MoveInput.x;
            float targetOffsetX = _defaultOffsetX;

            if (playerDirection < 0)
                targetOffsetX = -_xOffset;
            else if (playerDirection > 0)
                targetOffsetX = _xOffset;

            Vector3 currentOffset = _cmTransposer.m_FollowOffset;
            Vector3 targetOffset = new Vector3(targetOffsetX, currentOffset.y, currentOffset.z);
            _cmTransposer.m_FollowOffset = Vector3.SmoothDamp(currentOffset, targetOffset, ref _velocity, _smoothTime);
        }
    }
}