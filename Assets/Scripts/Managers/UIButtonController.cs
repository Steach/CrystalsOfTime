using CrystalOfTime.Systems.Command;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalOfTime.Systems.Managers.UI
{
    public class UIButtonController : MonoBehaviour
    {
        [SerializeField] private UIManager _UIManager;
        [Space]
        [Header("Menu Buttons")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [Space]
        [Header("Settings Buttons")]
        [SerializeField] private Button _returnButton;
        [Space]
        [Header("DeathScreen Buttons")]
        [SerializeField] private Button _restartButtonDeath;
        [SerializeField] private Button _exitButtonDeath;


        public delegate void ResumeSuttonHandler();
        public event ResumeSuttonHandler ResumeButtonClickEvent;

        private ExecutorBase _restartExecutor;
        private ExecutorBase _settingsExecutor;
        private ExecutorBase _resumeExecutor;
        private ExecutorBase _exitExecutor;
        private ExecutorBase _returnExecutor;
        private ExecutorBase _restartDeathExecutor;
        private ExecutorBase _exitDeathExecutor;

        private void OnEnable()
        {
            _resumeExecutor = GetExecutor(_resumeButton);
            _restartExecutor = GetExecutor(_restartButton);
            _exitExecutor = GetExecutor(_exitButton);
            _settingsExecutor = GetExecutor(_settingsButton);
            _returnExecutor = GetExecutor(_returnButton);
            _restartDeathExecutor = GetExecutor(_restartButtonDeath);
            _exitDeathExecutor = GetExecutor(_exitButtonDeath);

            _resumeButton.onClick.AddListener(ExecuteResume);
            _restartButton.onClick.AddListener(ExecuteRestart);
            _settingsButton.onClick.AddListener(ExecuteSettings);
            _exitButton.onClick.AddListener(ExecuteExit);
            _returnButton.onClick.AddListener(ReturnExecutor);
            _restartButtonDeath.onClick.AddListener(RestartDeathScreenExecutor);
            _exitButtonDeath.onClick.AddListener(ExitDeathScreenExecutor);


            ResumeButtonClickEvent += _UIManager.ListenResumeButtinEvent;
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(ExecuteResume);
            _restartButton.onClick.RemoveListener(ExecuteRestart);
            _settingsButton.onClick.RemoveListener(ExecuteSettings);
            _exitButton.onClick.RemoveListener(ExecuteExit);
            _returnButton.onClick.RemoveListener(ReturnExecutor);
            _restartButtonDeath.onClick.RemoveListener(RestartDeathScreenExecutor);
            _exitButtonDeath.onClick.RemoveListener(ExitDeathScreenExecutor);

            ResumeButtonClickEvent -= _UIManager.ListenResumeButtinEvent;
        }

        private ExecutorBase GetExecutor(Button button)
        { return button.gameObject.GetComponent<ExecutorBase>(); }

        private void ExecuteResume()
        {
            ResumeButtonClickEvent?.Invoke();
            _resumeExecutor.Execute();
        } 
        private void ExecuteRestart() => _restartExecutor.Execute();
        private void ExecuteSettings() => _settingsExecutor.Execute();
        private void ExecuteExit() => _exitExecutor.Execute();
        private void ReturnExecutor() => _returnExecutor.Execute();
        private void RestartDeathScreenExecutor() => _restartDeathExecutor.Execute();
        private void ExitDeathScreenExecutor() => _exitDeathExecutor.Execute();

    }
}