using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class TurtleController : MonoBehaviour
    {
        [SerializeField] private EnemyAnimController _enemyAnimController;
        [SerializeField] private float _checkRadius;
        [SerializeField] private LayerMask _layerMask;

        public delegate void TurtlePlayerDetectionHandler(bool playerInNear);
        public event TurtlePlayerDetectionHandler TurtlePlayerDetectionTrigger;

        private void OnEnable()
        {
            _enemyAnimController.Init(this);
        }

        private void OnDisable()
        {
            _enemyAnimController.UnInit(this);
        }

        private void Update()
        {
            CheckPlayerDistance();
        }

        private void CheckPlayerDistance()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, _checkRadius, Vector3.zero, 0f, _layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                    TurtlePlayerDetectionTrigger?.Invoke(true);
                else
                    TurtlePlayerDetectionTrigger?.Invoke(false);
            }
            else
                TurtlePlayerDetectionTrigger?.Invoke(false);

            DrawCircle(transform.position, _checkRadius, Color.green);
        }

        private void DrawCircle(Vector2 center, float radius, Color color)
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
    }
}