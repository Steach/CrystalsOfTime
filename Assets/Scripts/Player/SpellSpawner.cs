using CrystalOfTime.Systems.InputSystem;
using UnityEngine;

namespace CrystalOfTime.Player.Spells
{
    public class SpellSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject _spellToSpawn;
        [SerializeField] private Vector2 _spawnOffset;

        private PlayerInputMethod _playerInput;

        private void OnEnable()
        {
            _playerInput = _playerTransform.GetComponent<PlayerInputMethod>();
            _playerInput.PlayerCastingSpellTrigger += SpawnSpell;
        }

        private void OnDisable()
        {
            _playerInput.PlayerCastingSpellTrigger -= SpawnSpell;
        }

        private void SpawnSpell(bool isFlippingX)
        {
            if (isFlippingX)
            {
                _spawnOffset = new Vector2(-_spawnOffset.x, _spawnOffset.y);
            }

            var spawnPosition = new Vector2(_playerTransform.position.x + _spawnOffset.x, _playerTransform.position.y + _spawnOffset.y);
            var CastedSpellGO = Instantiate(_spellToSpawn, spawnPosition, Quaternion.identity);
            var CastedSpellBeh = CastedSpellGO.GetComponent<ShockSpellBehaviour>();
            CastedSpellBeh.Init(isFlippingX);
        }
    }
}