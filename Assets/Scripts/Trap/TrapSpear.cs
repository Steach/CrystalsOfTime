using UnityEngine;

namespace CrystalOfTime.NPC.Traps
{
    public class TrapSpear : MonoBehaviour
    {
        [SerializeField] private Collider2D _trapCollider;
        private int _damage = 100;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerColliding>(out PlayerColliding playerColliding))
                    playerColliding.PlayerTakeDamage(_damage);
            }
        }
    }
}