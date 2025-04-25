using CrystalOfTime.NPC.Traps;
using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class SpawnAction : ActionBase
    {

        public override void Execute(object data = null)
        {
            if (data != null && data is SpawnObjectInformation)
            {
                var info = (SpawnObjectInformation)data;
                GameObject arrow = Instantiate(info.ObjectToSpawn, info.PositionToSpawn.position, Quaternion.Euler(info.RotationToSpawn));
                if (arrow.TryGetComponent<ArrowTrap>(out ArrowTrap arrowTrap))
                    arrowTrap.Init(info.DirectionRight);
            }
        }
    }
}