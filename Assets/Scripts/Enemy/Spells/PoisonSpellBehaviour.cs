using System.Collections;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis.Spells
{
    public class PoisonSpellBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private SpellAnimationController _animatorController;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _targetPosition;
        private Vector3 _direction;
        private bool _isHit = false;

        public void Init(Transform targetTransform)
        {
            if (_targetTransform == null)
            {
                _targetTransform = targetTransform;
                _targetPosition = _targetTransform.position;
                _direction = (_targetPosition - transform.position).normalized;

                if (transform.position.x < _targetPosition.x)
                    _spriteRenderer.flipX = false;
                else
                    _spriteRenderer.flipX = true;

                StartCoroutine(EndLifeTime());
            }
        }

        private void Update()
        {
            if(_targetTransform != null && !_isHit)
                MoveToTarget();

            if (transform.position == _targetPosition)
            {
                _isHit = true;
                _animatorController.ChangeHitTrigger();
                Destroy(gameObject, 1);
            }
        }

        private void MoveToTarget()
        {
            //var direction = (_targetPosition - transform.position).normalized;
            transform.position += _direction * _speed * Time.deltaTime;
            //Debug.Log(direction);
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
            else if (collision.CompareTag("Ground"))
            {
                _isHit = true;
                _animatorController.ChangeHitTrigger();
                Destroy(gameObject, 1);
            }
        }

        private IEnumerator EndLifeTime()
        {
            yield return new WaitForSeconds(5);
            _isHit = true;
            _animatorController.ChangeHitTrigger();
            Destroy(gameObject, 1);
        }
    }
}