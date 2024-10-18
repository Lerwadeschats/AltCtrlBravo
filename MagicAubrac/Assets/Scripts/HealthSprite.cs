using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _fullHeartSprite;
    [SerializeField] Sprite _midHeartSprite;
    [SerializeField] Sprite _noHeartSprite;

    private void Start()
    {
        if (_fullHeartSprite != null)
        {
            _spriteRenderer.sprite = _fullHeartSprite;
        }
    }

    public void ChangeSprite(float heartHealth)
    {
        if (heartHealth == 0f)
        {
            ChangeSpriteToNoHeart();
        } else
        {
            ChangeSpriteToMidHeart();
        }
    }

    void ChangeSpriteToNoHeart()
    {
        _spriteRenderer.sprite = _noHeartSprite;
    }

    void ChangeSpriteToMidHeart()
    {
        _spriteRenderer.sprite = _midHeartSprite;
    }
}
