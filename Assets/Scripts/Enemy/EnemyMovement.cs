using System.Linq;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LayerMask _checkedLayerMask;

        private Vector3 _target;
        private Vector2 _rayDirection;
        private float _rayDistance = 5f;
        private bool _playerInTarget = false;

        private void Start()
        {
            _target = _pointLeft.position;
            SpriteFlipper(transform.position, _target);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed * Time.deltaTime);

            CheckTarget();
            
            if (Vector3.Distance(transform.position, _target) < 0.1f && !_playerInTarget)
            {
                _target = _target == _pointLeft.position ? _pointRight.position : _pointLeft.position;
                SpriteFlipper(transform.position, _target);
            } 
        }

        private void SpriteFlipper(Vector3 currentTransform, Vector3 destinationTransform)
        {
            if (currentTransform.x > destinationTransform.x)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipX = true;
        }

        private void CheckTarget()
        {
            _rayDirection = _spriteRenderer.flipX ? Vector2.right : Vector2.left;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, _rayDirection, _rayDistance, _checkedLayerMask);

            if (hit.collider != null)
            {
                _target = hit.collider.transform.position;
                _playerInTarget = true;
            }
            else
                _playerInTarget = false;
                
            Debug.DrawLine(transform.position, transform.position + (Vector3)_rayDirection * _rayDistance, Color.green);
        }
    }
}