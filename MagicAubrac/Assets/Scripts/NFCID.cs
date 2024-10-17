using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFCID : MonoBehaviour
{
    private int _ID;
    
    public IngredientType GetIngredient()//temporaire
    {
        _ID = Random.Range(0, 4);
        IngredientType card=IngredientType.INVALID;
        Debug.Log(_ID);
        switch (_ID)
        {
            case 0:
                card = IngredientType.LEMON;
                break;
            case 1:
                card = IngredientType.ORANGE;
                break;
            case 2:
                card = IngredientType.VODKA;
                break;
            case 3:
                card = IngredientType.WINE;
                break;
        }
        return card;
    }
    // ici avec le nfc L'id sera disponible pendant 5 à 10secondes avant d'être reset a rien.
}
