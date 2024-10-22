using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerUI : MonoBehaviour
{
    private IngredientType _ingredient;
    private Image _image;

    public IngredientType Ingredient { get => _ingredient; }
    public Image Image { get => _image;}

    private void Awake()
    {
        _image = GetComponent<Image>();
        Change(IngredientType.INVALID);
    }
    public void ChangeColor(Color color)
    {
        _image.color =color;
    }
    public void Change(IngredientType ingredient )
    {
        switch (ingredient)
        {
            case IngredientType.INVALID:
                _image.color = new Color(1,1,1,0);
                break;
            case IngredientType.JUS_DE_SCARABEE:
                _image.color = new Color(0.87f, 0.75f, 0.15f);
                break;
            case IngredientType.HIBISCUS:
                _image.color = new Color(0.87f, 0.55f, 0.05f);
                break;
            case IngredientType.SUC_DE_CRANE:
                _image.color = new Color(0.35f, 0.08f, 0.13f);
                break;
            case IngredientType.MENTHE:
                _image.color = new Color(0.08f, 0.35f, 0.13f);
                break;
            case IngredientType.EAU_DU_NIL:
                _image.color = new Color(0.35f, 0.5f, 0.7f);
                break;
            case IngredientType.VENIN_DE_SCORPION:
                _image.color = new Color(0.25f, 0.03f, 0.25f);
                break;
        }
    }
}
