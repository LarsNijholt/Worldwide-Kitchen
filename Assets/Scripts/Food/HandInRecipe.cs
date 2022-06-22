using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInRecipe : MonoBehaviour
{
    public GameObject _recipeHandIn;
    public void YesButton()
    {
        print("yes");
    }
    public void NoButton()
    {
        print("no");
        StopInteraction();
    }
    public void StartInteraction()
    {
        Time.timeScale = 0f;
        _recipeHandIn.SetActive(true);
    }
    public void StopInteraction()
    {
        Time.timeScale = 1.0f;
        _recipeHandIn.SetActive(false);
    }

    
}
