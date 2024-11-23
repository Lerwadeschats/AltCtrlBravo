using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerUI : MonoBehaviour
{

    public Color GetColor(IngredientType ingredient)
    {
        switch (ingredient)
        {
            case IngredientType.INVALID:
                return new Color(1, 1, 1, 0);
            case IngredientType.JUS_DE_SCARABEE:
                return new Color(0.87f, 0.75f, 0.15f);
            case IngredientType.HIBISCUS:
                return new Color(0.87f, 0.55f, 0.05f);
            case IngredientType.SUC_DE_CRANE:
                return new Color(0.35f, 0.08f, 0.13f);             
            case IngredientType.MENTHE:
                return new Color(0.08f, 0.35f, 0.13f);
            case IngredientType.EAU_DU_NIL:
                return new Color(0.35f, 0.5f, 0.7f);
            case IngredientType.VENIN_DE_SCORPION:
                return new Color(0.25f, 0.03f, 0.25f);
        }
        return Color.black;
    }
    public Color GetMixColor(List<IngredientType> ingredients)
    {
        float ratio=1;
        Color c = Color.black;
        switch (ingredients.Count)
        {
            case 2:
                ratio = .5f;
                break;
            case 3:
                ratio = .33f;
                break;
            case 4:
                ratio = .25f;
                break;
            case 5:
                ratio = .20f;
                break;
        }
        if (ingredients.Contains(IngredientType.HIBISCUS))
        {
            c += ratio * GetColor(IngredientType.HIBISCUS);
        }
        if (ingredients.Contains(IngredientType.EAU_DU_NIL))
        {
            c += ratio * GetColor(IngredientType.EAU_DU_NIL);
        }
        if (ingredients.Contains(IngredientType.MENTHE))
        {
            c += ratio * GetColor(IngredientType.MENTHE);
        }
        if (ingredients.Contains(IngredientType.JUS_DE_SCARABEE))
        {
            c += ratio * GetColor(IngredientType.JUS_DE_SCARABEE);
        }
        if (ingredients.Contains(IngredientType.SUC_DE_CRANE))
        {
            c += ratio * GetColor(IngredientType.SUC_DE_CRANE);
        }
        if (ingredients.Contains(IngredientType.VENIN_DE_SCORPION))
        {
            c += ratio * GetColor(IngredientType.VENIN_DE_SCORPION);
        }
        return c;
    }
}

