using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    public int Crystals { get; private set; }
    public int Coins { get; private set; }
    public int Potions { get; private set; }

    public float PlayerHP { get; private set; }

    public delegate void GrabCoinTriggerEventHandler(int coin);
    public static event GrabCoinTriggerEventHandler GrabCoin;

    public delegate void GrabCrystalTriggerEventHandler(int crystal);
    public static event GrabCrystalTriggerEventHandler GrabCrystal;

    public delegate void GrabHealthTriggerEventHandler(int health);
    public static event GrabHealthTriggerEventHandler GrabHeath;

    public delegate void PlayerDamagedEventHandler(float currentHP);
    public event PlayerDamagedEventHandler PlayerDamaged;

    public delegate void PlayerDeathTriggerHandler(bool isDead);
    public static event PlayerDeathTriggerHandler PlayerDeath;

    private float TakedDamage = 10f;

    private void Awake()
    {
        PlayerHP = 100;
        Crystals = 0;
        Coins = 0;
        Potions = 0;
        GrabCoin?.Invoke(Coins);
        GrabCrystal?.Invoke(Crystals);
        GrabHeath?.Invoke(Potions);
    }

    private void Start()
    {
        GrabCoin?.Invoke(Coins);
        GrabCrystal?.Invoke(Crystals);
        GrabHeath?.Invoke(Potions);
    }

    private void Update()
    {
        if (PlayerHP <= 0)
            PlayerDeath?.Invoke(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crystal")
        {
            Crystals++;
            GrabCrystal?.Invoke(Crystals);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Coin")
        {
            Coins++;
            GrabCoin?.Invoke(Coins);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Potion")
        {
            Potions++;
            GrabHeath?.Invoke(Potions);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            if (PlayerHP > 0)
            {
                PlayerHP -= TakedDamage;
                PlayerDamaged?.Invoke(PlayerHP);
            }
            else if (PlayerHP <= 0)
            {
                PlayerDeath?.Invoke(true);
            }
        }
    }
}