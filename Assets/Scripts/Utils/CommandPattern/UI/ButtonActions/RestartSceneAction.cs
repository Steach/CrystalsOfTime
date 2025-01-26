using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrystalOfTime.Systems.Command.UIButtons
{
    public class RestartSceneAction : ActionBase
    {
        public override void Execute(object data = null)
        {
            var currentScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(currentScene);
            SceneManager.LoadScene(currentScene);
        }
    }
}