using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class EnemyMovement : MonoBehaviour
    {
        public bool BatIsCelling { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private LayerMask _checkedLayerMask;
        [Space]
        [Header("Patrol movement")]
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;        
        [Space]
        [Header("Bat movement")]
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Rigidbody2D _rb;
        [Space]
        [Header("Type of Enemy:")]
        [SerializeField] private bool _isGhost;
        [SerializeField] private bool _isBat;

        private Vector3 _target;
        private Vector2 _rayDirection;
        private float _rayDistance = 5f;
        private bool _playerInTarget = false;


        public void Init(EnemiesController controller)
        {
            controller.EnemyPlayerDetectionTrigger += ChangePlayerDetectionStatus;
            controller.EnemyGetPlayerTransformTrigger += ChangeTargetTransform;
        }

        public void UnInit(EnemiesController controller)
        {
            controller.EnemyPlayerDetectionTrigger -= ChangePlayerDetectionStatus;
            controller.EnemyGetPlayerTransformTrigger -= ChangeTargetTransform;
        }

        private void Start()
        {
            if (_isGhost)
            {
                _target = _pointLeft.position;
                SpriteFlipper(transform.position, _target);
            }
        }

        private void Update()
        {
            if (_isGhost)
                GhostMovement();

            if (_isBat)
                BatMovement();
        }

        //--------------------GHOST------------------------------------/
        private void GhostMovement()
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
        //-------------------------END GHOST---------------------------/


        private void BatMovement()
        {
            if (transform.position.x == _startTransform.position.x && transform.position.y == _startTransform.position.y)
                BatIsCelling = true;
            else
                BatIsCelling = false;

            if (!_playerInTarget)
                _target = _startTransform.position;

            Vector2 direction = (_target - transform.position).normalized;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, Vector3.zero, 0f, _checkedLayerMask);

            
            if (hit.collider != null)
            {
                direction = Vector2.Perpendicular(direction);
            }

            if(_playerInTarget || !BatIsCelling)
                transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;
        }

        private void ChangePlayerDetectionStatus(bool isNear) => _playerInTarget = isNear;
        private void ChangeTargetTransform(Transform playerTransform) => _target = playerTransform.position;
    }
}