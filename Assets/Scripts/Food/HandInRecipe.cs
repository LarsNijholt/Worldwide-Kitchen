using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInRecipe : MonoBehaviour
{
    public GameObject _recipeHandIn;
    // reference to inventory
    public void YesButton()
    {
        // Check all ingredients in inventory
        // Get like 100 points for every right ingredient
        // for every minute subtract like 10 points
        // display points and time

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
