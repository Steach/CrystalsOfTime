using CrystalOfTime.Systems.InputSystem;
using UnityEngine;

public class PlayerSpriteRendererController : MonoBehaviour
{
    private PlayerInputMethod _playerInput;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    { 
        _playerInput = this.gameObject.GetComponent<PlayerInputMethod>();
    }

    private void Update()
    {
        if(_playerInput.MoveInput.x > 0)
            _spriteRenderer.flipX = false;
        else if(_playerInput.MoveInput.x < 0)
            _spriteRenderer.flipX = true;
    }
}