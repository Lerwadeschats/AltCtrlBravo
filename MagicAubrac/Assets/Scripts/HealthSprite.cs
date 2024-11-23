using DG.Tweening;
using IIMEngine.SFX;
using UnityEngine;
using UnityEngine.UI;

public class HealthSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Image _image;
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
            if (_spriteRenderer != null)
                _spriteRenderer.sprite = _fullHeartSprite;
            if (_image != null)
                _image.sprite = _fullHeartSprite;
        }
    }

    public void ChangeSprite(float heartHealth)
    {
        if (heartHealth == 0f)
        {
            ChangeSpriteToNoHeart();
            if (_spriteRenderer != null)
                _spriteRenderer.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(()=>SFXsManager.Instance.PlaySound(clipCracks));
            if (_image != null)
                _image.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(() => SFXsManager.Instance.PlaySound(clipCracks));

        }
        else
        {
            ChangeSpriteToMidHeart();
            if (_spriteRenderer != null)
                _spriteRenderer.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(()=> SFXsManager.Instance.PlaySound(clipShatter));
            if (_image != null)
                _image.gameObject.transform.DOShakePosition(_shakeDuration, _shakeStrength).OnComplete(() => SFXsManager.Instance.PlaySound(clipShatter));
        }

    }

    void ChangeSpriteToNoHeart()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = _noHeartSprite;
        if (_image != null)
            _image.sprite = _noHeartSprite;
    }

    void ChangeSpriteToMidHeart()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = _midHeartSprite;
        if (_image != null)
            _image.sprite = _midHeartSprite;
    }
}
