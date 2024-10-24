using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using IIMEngine.SFX;
using UnityEngine;

public class HealthSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _fullHeartSprite;
    [SerializeField] Sprite _midHeartSprite;
    [SerializeField] Sprite _noHeartSprite;
    [SerializeField] float _shakeDuration = 0.2f;
    [SerializeField] float _shakeStrength = 0.4f;

    [SerializeField] string clipCracks;
    [SerializeField] string clipShatter;

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
            _spriteRenderer.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(()=>SFXsManager.Instance.PlaySound(clipCracks));

        }
        else
        {
            ChangeSpriteToMidHeart();
            _spriteRenderer.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(()=> SFXsManager.Instance.PlaySound(clipShatter));

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
