using UnityEngine;

namespace CrystalOfTime.Systems.Command.UIButtons
{
    public class ExitGameAction : ActionBase
    {
        public override void Execute(object data = null)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}