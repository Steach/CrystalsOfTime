using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

namespace CrystalOfTime.NPC.Enemeis.Spells
{
    public class EnemiesSpellCaster : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private bool _canSpawn = true;

        public void Init(EnemyMovement enemyMovement)
        {
            enemyMovement.DistanceForSpellCastingTrigger += SpawnSpell;
        }

        public void UnInit(EnemyMovement enemyMovement)
        {
            enemyMovement.DistanceForSpellCastingTrigger += SpawnSpell;
        }

        private void SpawnSpell(Transform _spawnTransform, Transform target) 
        {
            if (_canSpawn)
            {
                _canSpawn = false;
                var spellObject = Instantiate(_prefab, _spawnTransform.position, Quaternion.identity);

                if (spellObject.TryGetComponent<PoisonSpellBehaviour>(out PoisonSpellBehaviour spellBeh))
                {
                    spellBeh.Init(target);
                }
                StartCoroutine(TimerToSpawn());
            }
        }

        private IEnumerator TimerToSpawn()
        {
            yield return new WaitForSeconds(2);
            _canSpawn = true;
        }
    }
}