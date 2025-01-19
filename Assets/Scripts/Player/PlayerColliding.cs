using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    public int Crystals { get; private set; }
    public int Coins { get; private set; }

    public float PlayerHP { get; private set; }

    public delegate void GrabCoinTriggerEventHandler(int coin);
    public static event GrabCoinTriggerEventHandler GrabCoin;

    public delegate void GrabCrystalTriggerEventHandler(int crystal);
    public static event GrabCrystalTriggerEventHandler GrabCrystal;

    public delegate void GrabHealthTriggerEventHandler();
    public static event GrabHealthTriggerEventHandler GrabHeath;

    public delegate void PlayerDamagedEventHandler(float currentHP);
    public event PlayerDamagedEventHandler PlayerDamaged;

    public delegate void PlayerPortalEventHandler();
    public event PlayerPortalEventHandler PlayerPortalEevent;

    public delegate void PlayerDeathTriggerHandler(bool isDead);
    public static event PlayerDeathTriggerHandler PlayerDeath;

    private float _takedDamage = 10f;
    private float _healPlayer = 30f;
    private float _maxPlayerHP = 100;

    private void Awake()
    {
        PlayerHP = _maxPlayerHP;
        Crystals = 0;
        Coins = 0;
        GrabCoin?.Invoke(Coins);
        GrabCrystal?.Invoke(Crystals);
    }

    private void Start()
    {
        GrabCoin?.Invoke(Coins);
        GrabCrystal?.Invoke(Crystals);
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
            if (PlayerHP < _maxPlayerHP)
            {
                var missingHP = _maxPlayerHP - PlayerHP;

                if (missingHP > _healPlayer)
                    PlayerHP += _healPlayer;
                else if (missingHP < _healPlayer)
                    PlayerHP = _maxPlayerHP;

                Destroy(collision.gameObject);
                PlayerDamaged?.Invoke(PlayerHP);
                GrabHeath?.Invoke();
            }
        }
        else if (collision.tag == "Enemy")
        {
            if (PlayerHP > 0)
            {
                PlayerHP -= _takedDamage;
                PlayerDamaged?.Invoke(PlayerHP);
            }
            else if (PlayerHP <= 0)
            {
                PlayerDeath?.Invoke(true);
            }
        }
        else if (collision.tag == "Portal")
        {
            PlayerPortalEevent?.Invoke();
        }
    }
}