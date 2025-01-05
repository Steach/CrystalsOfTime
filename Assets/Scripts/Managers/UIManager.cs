using TMPro;
using UnityEngine;
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
        [SerializeField] private TextMeshProUGUI _potionCount;
        [SerializeField] private Slider _playerHPSlider;

        private PlayerColliding _playerColliding;
        private float _maxHPValue;

        private void Start()
        {
            ConfigureHPSlider();
        }

        private void OnEnable()
        {
            _playerColliding = GetPlayerColliding();
            PlayerColliding.GrabItem += UpdateUI;
            _playerColliding.PlayerDamaged += UpdateHpSlider;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabItem -= UpdateUI;
            _playerColliding.PlayerDamaged -= UpdateHpSlider;
        }

        private void UpdateUI(int coin, int crystal, float potion)
        {
            _coinCount.text = coin.ToString();
            _crystalCount.text = crystal.ToString();
            _potionCount.text = potion.ToString();
        }

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
    }
}