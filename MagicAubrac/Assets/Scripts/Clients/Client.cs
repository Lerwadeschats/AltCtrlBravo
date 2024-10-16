using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Recipe _recipe;

    public string GetDebugString()
    {
        return _recipe.GetDebugString();
    }
}
