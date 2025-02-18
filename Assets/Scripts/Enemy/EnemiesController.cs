using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public abstract class EnemiesController : MonoBehaviour
    {
        [SerializeField] protected float _checkRadius;
        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected float _damage;

        public delegate void EnemyPlayerDetectionHandler(bool playerInNear);
        public event EnemyPlayerDetectionHandler EnemyPlayerDetectionTrigger;

        protected void CheckPlayerDistanceOnCircle()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _checkRadius, Vector3.zero, 0f, _layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                    EnemyPlayerDetectionTrigger?.Invoke(true);
                else
                    EnemyPlayerDetectionTrigger?.Invoke(false);
            }
            else
                EnemyPlayerDetectionTrigger?.Invoke(false);

            DrawCircle(transform.position, _checkRadius, Color.green);
        }

        protected void DrawCircle(Vector2 center, float radius, Color color)
        {
            int segments = 100;
            float angle = 2f * Mathf.PI / segments;
            Vector3[] points = new Vector3[segments + 1];

            for (int i = 0; i <= segments; i++)
            {
                float x = center.x + radius * Mathf.Cos(angle * i);
                float y = center.y + radius * Mathf.Sin(angle * i);
                points[i] = new Vector3(x, y, 0);
            }

            for (int i = 0; i < segments; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color);
            }
        }

        protected void KnockBackThePlayer(Collision2D collision)
        {
            
        }

        protected void DamagedThePlayer(Collision2D collision)
        {
            var playerColliding = collision.gameObject.GetComponent<PlayerColliding>();
            playerColliding.PlayerTakeDamage(_damage);
        }
    }
}

