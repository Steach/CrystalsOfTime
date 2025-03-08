using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis.Spells
{
    public class SpellAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorController _controllerMove;
        [SerializeField] private AnimatorController _controllerHit;

        private bool _isHit = false;
        private bool _hitWasOnce = false;

        private void Start()
        {
            _animator.runtimeAnimatorController = _controllerMove;
        }

        private void Update()
        {
            if (_isHit && !_hitWasOnce)
            {
                _animator.runtimeAnimatorController = _controllerHit;
                _hitWasOnce = true;
            }
                
        }

        public void ChangeHitTrigger()
        {
            if (!_hitWasOnce)
                _isHit = true;
        }
    }
}