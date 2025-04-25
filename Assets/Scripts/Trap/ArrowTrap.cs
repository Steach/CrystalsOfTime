using UnityEngine;

namespace CrystalOfTime.NPC.Traps
{
    public class ArrowTrap : DamagedTrap
    {
        [SerializeField] private float _moveSpeed;
        private bool _isInited = false;
        private float _defaultSpeed = 0.3f;

        public void Init(bool rightDirection)
        {
            if (!_isInited)
            {
                _isInited = true;

                if (_moveSpeed < 0)
                {
                    _moveSpeed *= -1;
                }
                else if (_moveSpeed == 0)
                {
                    _moveSpeed = _defaultSpeed;
                }

                if (!rightDirection)
                {
                    _moveSpeed *= -1;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isInited)
                Move();
        }

        private void Move()
        {
            transform.position = new Vector2(transform.position.x + _moveSpeed, transform.position.y);
        }
    }
}