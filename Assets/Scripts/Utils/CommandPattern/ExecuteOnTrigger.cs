using UnityEngine;

namespace CrystalOfTime.Systems.Command
{
    public class ExecuteOnTrigger : MonoBehaviour
    {
        [SerializeField] private ActionBase[] _actions;
        [SerializeField] private ConditionBase _condition;
        [SerializeField] private bool _checkByTag;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_checkByTag)
            {
                if (_condition.Check(collision.gameObject.tag))
                {
                    foreach (var action in _actions)
                    {
                        if(collision.gameObject.TryGetComponent<PlayerColliding>(out PlayerColliding playerColliding))
                            action.Execute(playerColliding);
                    }
                }
            }
        }
    }
}