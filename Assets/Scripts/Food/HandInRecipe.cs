using UnityEngine;

public class HandInRecipe : MonoBehaviour
{
    public GameObject RecipeHandIn;

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
        RecipeHandIn.SetActive(true);
    }
    public void StopInteraction()
    {
        Time.timeScale = 1.0f;
        RecipeHandIn.SetActive(false);
    }

    
}
