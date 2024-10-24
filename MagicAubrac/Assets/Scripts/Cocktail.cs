using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cocktail : MonoBehaviour
{
    [SerializeField] InputPlayer _inputs;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] float _imageDisappear = 0.5f;
    Coroutine _coroutine;

    private void Start()
    {
        _inputs.OnDrinkFinished += OnDrinkFinished;
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
    }

}
