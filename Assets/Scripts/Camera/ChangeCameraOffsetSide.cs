using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

namespace CrystalOfTime.Systems.Camera
{
    public class ChangeCameraOffsetSide : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _vcamera;
        [SerializeField] private Transform _playerTransform;

        [SerializeField] private float _xOffset;
        [SerializeField] private float _defaultOffsetX;
        [SerializeField] private float _smoothTime;

        private CinemachineTransposer _cmTransposer;
        

        

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}