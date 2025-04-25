using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class DamagePlayerAction : ActionBase
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _destroyAfterCollide;
        public override void Execute(object data = null)
        {
            if (data is PlayerColliding)
            {
                PlayerColliding playerColliding = (PlayerColliding)data;
                playerColliding.PlayerTakeDamage(_damage);

                if(_destroyAfterCollide)
                    Destroy(gameObject);
            }
        }
    }
}