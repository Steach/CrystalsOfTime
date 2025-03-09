using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis.Spells
{
    public class PoisonSpellBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private SpellAnimationController _animatorController;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isHit = false;

        public void Init(Transform targetTransform)
        {
            if (_targetTransform == null)
                _targetTransform = targetTransform;
        }

        private void Update()
        {
            if(_targetTransform != null && !_isHit)
                MoveToTarget();
        }

        private void MoveToTarget()
        {
            var direction = (_targetTransform.position - transform.position).normalized;
            transform.position += (Vector3)direction * _speed * Time.deltaTime;
            if (transform.position.x < _targetTransform.position.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !_isHit)
            {
                _isHit = true;
                _animatorController.ChangeHitTrigger();

                if (collision.gameObject.TryGetComponent<PlayerColliding>(out PlayerColliding playerColliding))
                    playerColliding.PlayerTakeDamage(20);

                Destroy(gameObject, 1);
            }    
        }
    }
}