using System;
using Unity.VisualScripting;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public event Action onBecomeInvisible;
    public float sizeX => _spriteRenderer.bounds.size.x;
    public float halfSizeY => _spriteRenderer.bounds.size.y / 2;


    public bool isInPool = false;

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private void OnBecameInvisible()
    {
        onBecomeInvisible?.Invoke();
    }
}
