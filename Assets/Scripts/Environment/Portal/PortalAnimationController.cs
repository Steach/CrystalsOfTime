using CrystalOfTime.Systems.Managers;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.Systems.Environments.Portal
{
    public class PortalAnimationController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _portalSpriteRenderer;
        [SerializeField] private Collider2D _portalCollider;
        [Space]
        [Header("Animator Controllers")]
        [SerializeField] private AnimatorController _openingPortalController;
        [SerializeField] private AnimatorController _openedPortalController;
        [Space]
        [Header("Animation timings")]
        [SerializeField] private float _openingPortalTime;
        [SerializeField] private float _openedPortalTime;

        private bool _isInited = false;

        public void InitPortal()
        {
            if (!_isInited)
            {
                _isInited = true;
                _portalCollider.enabled = true;
                StartCoroutine(OpenPortal());
            }
        }

        public void ClosePortal()
        {
            _portalCollider.enabled = false;
            _animator.runtimeAnimatorController = null;
            _portalSpriteRenderer.sprite = null;
        }

        private void ChangeAnimation(AnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }

        private IEnumerator OpenPortal()
        {
            ChangeAnimation(_openingPortalController);
            yield return new WaitForSeconds(_openingPortalTime);
            ChangeAnimation(_openedPortalController);
        }
    }
}