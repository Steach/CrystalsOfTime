using CrystalOfTime.Systems.Command;
using CrystalOfTime.Systems.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CrystalOfTime.Systems.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
        [Header("Count text fields")]
        [SerializeField] private TextMeshProUGUI _crystalCount;
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private Slider _playerHPSlider;
        [SerializeField] private ExecutorTrigger _executorTrigger;

        private PlayerColliding _playerColliding;
        private float _maxHPValue;

        private UIInputController _inputController;
        private bool _menuIsOpened = false;

        private void Start()
        {
            ConfigureHPSlider();
        }

        private void OnEnable()
        {
            _inputController = new UIInputController();
            _inputController.Enable();
            _playerColliding = GetPlayerColliding();

            PlayerColliding.GrabCoin += UpdateUICoin;
            PlayerColliding.GrabCrystal += UpdateUICrystal;

            _playerColliding.PlayerDamaged += UpdateHpSlider;
            _inputController.Menu.Open.started += OpenMenu;
        }

        private void OnDisable()
        {
            _inputController.Disable();
            PlayerColliding.GrabCoin -= UpdateUICoin;
            PlayerColliding.GrabCrystal -= UpdateUICrystal;
            _playerColliding.PlayerDamaged -= UpdateHpSlider;


            _inputController.Menu.Open.started -= OpenMenu;
        }

        private void UpdateUICoin(int coin) => _coinCount.text = coin.ToString();

        private void UpdateUICrystal(int crystal) => _crystalCount.text = crystal.ToString();


        private PlayerColliding GetPlayerColliding()
        {
            GameObject player = _gameManager.GetPlayer();
            return player.GetComponent<PlayerColliding>();
        }

        private void ConfigureHPSlider()
        {
            _maxHPValue = _playerColliding.PlayerHP;
            _playerHPSlider.maxValue = _maxHPValue;
            _playerHPSlider.value = _playerHPSlider.maxValue;
        }

        private void UpdateHpSlider(float newHPCount)
        {
            _playerHPSlider.value = newHPCount;
        }

        public void ListenResumeButtinEvent()
        {
            _menuIsOpened = !_menuIsOpened;
        }

        private void OpenMenu(InputAction.CallbackContext callback)
        {
            _menuIsOpened = !_menuIsOpened;
            _executorTrigger.Execute(_menuIsOpened);
        }
    }
}