using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    public int Crystals { get; private set; }
    public int Coins { get; private set; }
    public float Potions { get; private set; }

    public float PlayerHP { get; private set; }

    public delegate void TriggerEvenyHandler(int coin, int crystal, float potion);
    public static event TriggerEvenyHandler GrabItem;

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
        GrabItem?.Invoke(Coins, Crystals, Potions);
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
            GrabItem?.Invoke(Coins, Crystals, Potions);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Coin")
        {
            Coins++;
            GrabItem?.Invoke(Coins, Crystals, Potions);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Potion")
        {
            Potions++;
            GrabItem?.Invoke(Coins, Crystals, Potions);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            if (PlayerHP > 0)
            {
                PlayerHP -= TakedDamage;
                Debug.Log(PlayerHP);
                PlayerDamaged?.Invoke(PlayerHP);
            }
            else if (PlayerHP <= 0)
            {
                PlayerDeath?.Invoke(true);
            }
        }
    }
}