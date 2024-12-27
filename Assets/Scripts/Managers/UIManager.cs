using TMPro;
using UnityEngine;

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

        private void OnEnable()
        {
            PlayerColliding.GrabItem += UpdateUI;
        }

        private void OnDisable()
        {
            PlayerColliding.GrabItem -= UpdateUI;
        }

        private void UpdateUI(int coin, int crystal, float potion)
        {
            _coinCount.text = coin.ToString();
            _crystalCount.text = crystal.ToString();
            _potionCount.text = potion.ToString();
        }
    }
}