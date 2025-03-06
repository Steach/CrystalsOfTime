using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis.Spells
{
    public class PoisonSpellBehaviour : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _targetTransform;

        public void Init(Transform targetTransform)
        {
            if (_targetTransform == null)
                _targetTransform = targetTransform;
        }

        private void Update()
        {
            if(_targetTransform != null)
                MoveToTarget();
        }

        private void MoveToTarget()
        {
            var direction = (_targetTransform.position - transform.position).normalized;
            transform.position += (Vector3)direction * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player");
                Destroy(gameObject);
            }    
        }
    }
}