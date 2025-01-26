using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class ExecutorTrigger : MonoBehaviour
    {
        [SerializeField] private ActionBase[] _actionsA;
        [SerializeField] private ActionBase[] _actionsB;

        public virtual void Execute(object data = null)
        {
            if (data != null)
            {
                if (data is bool)
                {
                    bool trigger = (bool)data;
                    if (trigger)
                    {
                        foreach (var action in _actionsA)
                            action.Execute();
                    }
                    else if (!trigger)
                    {
                        foreach (var action in _actionsB)
                            action.Execute();
                    }
                }
            }
        }
    }
}