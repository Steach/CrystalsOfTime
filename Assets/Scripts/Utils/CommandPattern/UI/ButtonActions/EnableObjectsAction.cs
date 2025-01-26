using UnityEngine;

namespace CrystalOfTime.Systems.Command.UIButtons
{
    public class EnableObjectsAction : ActionBase
    {
        [SerializeField] private GameObject[] _gameObjectsForEnable;
        public override void Execute(object data = null)
        {
            EnableObjects();
        }

        private void EnableObjects()
        {
            foreach (var obj in _gameObjectsForEnable)
                obj.SetActive(true);
        }
    }
}