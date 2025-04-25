using CrystalOfTime.Systems.Command;
using UnityEngine;

namespace CrystalOfTime.NPC.Traps
{
    public class DamagedTrap : MonoBehaviour
    {
        [SerializeField] protected ExecuteOnTrigger _executor;
    }
}