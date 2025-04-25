using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class DamagePlayerAction : ActionBase
    {
        [SerializeField] private float damage;
        public override void Execute(object data = null)
        {
            if (data is PlayerColliding)
            {
                PlayerColliding playerColliding = (PlayerColliding)data;
                playerColliding.PlayerTakeDamage(damage);
            }
        }
    }
}