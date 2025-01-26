using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public abstract class ActionBase : MonoBehaviour
    {
        public abstract void Execute(object data = null);
    }
}