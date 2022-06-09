using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Food
{
    public class RecipeBookController : MonoBehaviour
    {
        [SerializeField] private GameObject _interactPopup;

        private bool _canInteract;
        private bool _showingBook;
        private bool _firstOpen;

        private int _collided;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _collided++;
                Invoke(_collided > 0 ? "ShowPopup" : "HidePopup", 0f);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _collided--;
                Invoke(_collided > 0 ? "ShowPopup" : "HidePopup", 0f);
            }
        }
        private void ShowPopup()
        {
            _interactPopup.SetActive(true);
            _canInteract = true;
        }
        private void HidePopup()
        {
            _interactPopup.SetActive(false);
            _canInteract = false;
            HideRecipeBook();
        }

        private void Update()
        {
            if (!_canInteract) return;
            if (Input.GetKeyDown(KeyCode.Space))
                Invoke(_showingBook ? "HideRecipeBook" : "ShowRecipeBook", 0f);
        }

        private void ShowRecipeBook()
        {
            _showingBook = true;
            if (!_firstOpen)
            {
                Debug.Log("Show Recipe book (no silhouette)");
                _firstOpen = true;
            }
            else
            {
                Debug.Log("Show Recipe book (yes silhouette)");
            }
        }
        private void HideRecipeBook()
        {
            _showingBook = false;
            Debug.Log("Hide Recipe book");
        }
    } 
}
