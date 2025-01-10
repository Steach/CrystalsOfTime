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

            PlayerColliding.GrabCoin += UpdateUICoin;
            PlayerColliding.GrabCrystal += UpdateUICrystal;

            _playerColliding.PlayerDamaged += UpdateHpSlider;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabCoin -= UpdateUICoin;
            PlayerColliding.GrabCrystal -= UpdateUICrystal;
            _playerColliding.PlayerDamaged -= UpdateHpSlider;
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
    }
}