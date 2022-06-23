using Assets.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Food
{
    public class IngredientList : MonoBehaviour
    {
        [SerializeField] private List<BaseIngredient> _allIngredients;
        [SerializeField] private InventorySystem _playerInventory;
        [SerializeField] private int _rating = 0; //Placeholder until we have a better rating method.

        private BaseIngredient _foodToCook;

        private void Awake()
        {
           InitializeList();
        }

        private void InitializeList()
        {
            int selectFood = Random.Range(0, _allIngredients.Count);
            print(selectFood);
            _foodToCook = _allIngredients[selectFood];
            print(_foodToCook);
        }

        public void GetRated()
        {
            List<BaseIngredient> playerInventory = _playerInventory.GetInventory();
            for (int i = 0; i < _allIngredients.Count; i++)
            {
                for (int j = 0; j < playerInventory.Count; j++)
                {
                    if (playerInventory[j] == _allIngredients[i])
                    {
                        _rating += 1; // To make sure it only executes once when debugging.
                    }
                }
            }
        }

        public List<BaseIngredient> GetIngredients() { return _allIngredients; }
        public int GetRating() { return _rating; }
    } 
}
