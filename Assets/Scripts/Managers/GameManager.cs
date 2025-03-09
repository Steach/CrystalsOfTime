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
            Debug.Log("Crystal grab event");
            if (_portal.TryGetComponent<PortalAnimationController>(out PortalAnimationController portalController))
            {
                Debug.Log("Get portal component");
                var currentScene = SceneManager.GetActiveScene().buildIndex;

                if (currentScene == 0)
                    ManagePortal(portalController, (int)CrystalCount.FirstLevel, crystal);
                if (currentScene == 1)
                    ManagePortal(portalController, (int)CrystalCount.SecondLevel, crystal);
            }
        }

        private void LevelComplete()
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;

            if (CheckNextScene(currentScene))
                SceneManager.LoadScene(currentScene + 1);
            else
                Debug.Log("Game is complete!");
                
        }

        private bool CheckNextScene(int sceneIndex)
        {
            //Debug.Log($"Scene index: {sceneIndex}");
            //Debug.Log($"Count scene in manager: {SceneManager.sceneCountInBuildSettings}");
            return sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings - 1;
        }

        private void ManagePortal(PortalAnimationController portalController, int neededCount, int currentCount)
        {
            //Debug.Log($"neededCount: {neededCount}, currentCount: {currentCount}");

            if (currentCount >= neededCount)
                portalController.InitPortal();
            else
                portalController.ClosePortal();
        }

        public GameObject GetPlayer()
        { return _player; }

        public enum CrystalCount
        {
            FirstLevel = 3,
            SecondLevel = 5
        }
    }
}