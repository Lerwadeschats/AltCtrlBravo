using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _fullHeartSprite;
    [SerializeField] Sprite _midHeartSprite;
    [SerializeField] Sprite _noHeartSprite;
    [SerializeField] float _shakeDuration = 0.2f;
    [SerializeField] float _shakeStrength = 0.4f;

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
        _spriteRenderer.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength);
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
