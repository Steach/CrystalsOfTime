using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _target;

        private void Start()
        {
            _target = _pointLeft.position;
            SpriteFlipper(transform.position, _target);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _moveSpeed * Time.deltaTime);

            
            if (Vector3.Distance(transform.position, _target) < 0.1f)
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
    }
}