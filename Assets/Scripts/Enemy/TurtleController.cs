using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis
{
    public class TurtleController : EnemiesController
    {
        [SerializeField] private Collider2D _collider;
        [Header("Animator")]
        //[SerializeField] private EnemyAnimController _enemyAnimController;

        [SerializeField] private float _knockbackForce;

        private void OnEnable()
        {
            _enemyAnimationController.Init(this);
        }

        private void OnDisable()
        {
            _enemyAnimationController.UnInit(this);
        }

        private void Update()
        {
            CheckPlayerDistanceOnCircle();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRB.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);

                DamagedThePlayer(collision);
            }
        }
    }
}