using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Food
{
    public class CookPot : MonoBehaviour
    {
        private bool _canInteract;

        [SerializeField] private IngredientList _ingredientList;
        string Cooking;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _canInteract = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _canInteract = false;
            }
        }
      

        private void Update()
        {
            if (!_canInteract) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ingredientList.GetRated();
                Cooking = string.Format("Start cooking: Rating:", _ingredientList.GetRating());
                print(Cooking);
            }
        }
    }
} 
