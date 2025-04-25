using CrystalOfTime.Systems.Command;
using System.Collections;
using UnityEngine;

namespace CrystalOfTime.NPC.Traps
{
    public class HiddingArrowTrap : MonoBehaviour
    {
        [SerializeField] private SpawnObjectInformation _spawnObjectInformation;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private ExecutorBase _executor;

        private void Start()
        {
            StartCoroutine(SpawnArrowsTrap());
        }

        IEnumerator SpawnArrowsTrap()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                _executor.Execute(_spawnObjectInformation);
            }
        }
    }
}