using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class ExecutorBase : MonoBehaviour
    {
        [SerializeField] private ActionBase[] _actions;
        [SerializeField] private ConditionBase _condition;

        public virtual void Execute(object data = null)
        {
            if (_condition == null || _condition.Check(data))
            {
                foreach (var action in _actions)
                {
                    action.Execute(data);
                }
            }
        }
    }
}