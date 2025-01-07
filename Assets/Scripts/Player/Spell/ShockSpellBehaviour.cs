using CrystalOfTime.NPC.Enemeis;
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
        [SerializeField] private ProjectTags[] _notCollisionTags;
        [SerializeField] private float _damage;

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
            if (CheckCollisionTag(collision.tag))
            {
                if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
                    enemyController.GetHit(_damage);


                _isHit = true;
                _animator.runtimeAnimatorController = _hitController;
                StartCoroutine(DestroySpell());
            }
                
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckCollisionTag(collision.collider.tag))
            {
                _isHit = true;
                _animator.runtimeAnimatorController = _hitController;
                StartCoroutine(DestroySpell());
            }
        }

        private bool CheckCollisionTag(string collisionTag)
        {
            foreach (var tag in _notCollisionTags)
            {
                var strTag = tag.ToString();

                if (collisionTag == strTag)
                {
                    Debug.Log(strTag);
                    return false;
                }
            }
            return true;
        }

        private IEnumerator DestroySpell()
        {
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }
    }
}