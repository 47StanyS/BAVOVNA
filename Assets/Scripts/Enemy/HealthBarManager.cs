using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRendererFill;

    private float _sizeHeigt;

    private void Awake()
    {
        _sizeHeigt = _spriteRendererFill.size.y;
    }

    public void SetHealth(float _currentHealth, float _maxHealth)
    {
        _spriteRendererFill.size = new Vector2(_currentHealth / _maxHealth, _sizeHeigt);
    }

}
