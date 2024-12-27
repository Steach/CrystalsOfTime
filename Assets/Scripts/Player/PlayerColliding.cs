using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    public int Crystals { get; private set; }
    public int Coins { get; private set; }
    public float Potions { get; private set; }

    public delegate void TriggerEvenyHandler(int coin, int crystal, float potion);
    public static event TriggerEvenyHandler GrabItem;

    private void Start()
    {
        Crystals = 0;
        Coins = 0;
        Potions = 0;
        GrabItem?.Invoke(Coins, Crystals, Potions);
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
    }
}