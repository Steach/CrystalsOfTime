using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using CrystalOfTime.Systems.InputSystem;

namespace CrystalOfTime.NPC.Traps
{
    public class HiddingTrap : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent<PlayerInputMethod>(out PlayerInputMethod playerInput))
                {
                    StartCoroutine(ActivationTrap(playerInput));
                }
            }
        }

        IEnumerator ActivationTrap(PlayerInputMethod playerInput)
        {
            var oldSpeed = playerInput.ChangeMovingSpeed(0.3f);
            yield return new WaitForSeconds(0.1f);
            playerInput.ReturnMoveSpeed(oldSpeed);
            gameObject.SetActive(false);
        }
    }
}