using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

namespace CrystalOfTime.Player.Spells
{
    public class ShockSpellBehaviour : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorController _hitController;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isHit = false;

        public void Init(bool isFlipping)
        {
            if (isFlipping)
            {
                _spriteRenderer.flipX = isFlipping;
                _moveSpeed = -_moveSpeed;
            }
        }

        private void Update()
        {
            if (!_isHit)
                SpellMove();
        }

        private void SpellMove()
        {
            transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime, transform.position.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "Player" && collision.tag != "MainCamera")
            {
                _isHit = true;
                _animator.runtimeAnimatorController = _hitController;
                StartCoroutine(DestroySpell());
            }
                
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag != "Player" && collision.collider.tag != "MainCamera")
            {
                _isHit = true;
                _animator.runtimeAnimatorController = _hitController;
                StartCoroutine(DestroySpell());
            }
        }

        private IEnumerator DestroySpell()
        {
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }
    }
}