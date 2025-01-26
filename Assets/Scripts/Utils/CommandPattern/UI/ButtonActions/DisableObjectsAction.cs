using UnityEngine;

namespace CrystalOfTime.Systems.Command.UIButtons
{
    public class DisableObjectsAction : ActionBase
    {
        [SerializeField] private GameObject[] _gameObjectsForDisable;

        public override void Execute(object data = null)
        {
            DisableObjects();
        }

        private void DisableObjects()
        {
            foreach (var obj in _gameObjectsForDisable)
                obj.SetActive(false);
        }
    }
}