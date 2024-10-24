using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail : MonoBehaviour
{
    [SerializeField] InputPlayer _inputs;
    [SerializeField] Color _colorFail;
    [SerializeField] Color _colorTasteOnly;
    [SerializeField] Color _colorRunesOnly;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _imageDisappear = 0.5f;
    Coroutine _coroutine;

    private void Start()
    {
        _inputs.OnDrinkFinished += OnDrinkFinished;
        _inputs.OnDrinkSucceeded += OnDrinkSucceeded;
        _inputs.OnDrinkRunesOnly += OnDrinkRunesOnly;
        _inputs.OnDrinkTasteOnly += OnDrinkTasteOnly;
        _inputs.OnDrinkFailed += OnDrinkFailed;
    }

    private void OnDrinkFailed()
    {
        _spriteRenderer.color = _colorFail;
    }

    private void OnDrinkTasteOnly()
    {
        _spriteRenderer.color = _colorTasteOnly;
    }

    private void OnDrinkRunesOnly()
    {
        _spriteRenderer.color = _colorRunesOnly;
    }

    private void OnDrinkSucceeded()
    {
        _spriteRenderer.color = Color.white;
    }

    private void OnDrinkFinished(Recipe recipe)
    {
        _spriteRenderer.sprite = recipe.UIRecipeSprite;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(RoutineRemoveSprite());
    }

    IEnumerator RoutineRemoveSprite()
    {
        //Change for pause
        //Change color depending on finished well or not
        yield return new WaitForSeconds(_imageDisappear);
        _spriteRenderer.sprite = null;
        _spriteRenderer.color = Color.white;
    }

}
