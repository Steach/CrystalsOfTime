using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public abstract class ConditionBase : MonoBehaviour
    {
        public abstract bool Check(object data = null);
    }
}