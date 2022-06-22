using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class CharacterSelect : MonoBehaviour
    {
        [SerializeField] private Sprite _europeSprite;
        [SerializeField] private Sprite _oceaniaSprite;
        [SerializeField] private Sprite _africaSprite;
        [SerializeField] private Sprite _asiaSprite;
        [SerializeField] private List<Button> _playerOneButtons;
        [SerializeField] private List<Button> _playerTwoButtons;
        public Selection _selection = Selection.none;

        string key = "Selection Player 1";

        public void SelectEurope()
        {
            _selection = Selection.europe;
            StoreSelection(key);
            ToggleInactive(_playerOneButtons, _playerTwoButtons);
        }

        public void SelectOceania()
        {
            _selection = Selection.Oceania;
            StoreSelection(key);
            ToggleInactive(_playerOneButtons, _playerTwoButtons);
        }

        public void SelectAfrica()
        {
            _selection = Selection.Africa;
            StoreSelection(key);
            ToggleInactive(_playerOneButtons, _playerTwoButtons);
        }

        public void SelectAsia()
        {
            _selection = Selection.Asia;
            StoreSelection(key);
            ToggleInactive(_playerOneButtons, _playerTwoButtons);
        }

        private void ToggleInactive(List<Button> ActiveList, List<Button> InactiveList)
        {
            bool toggle = true;
            foreach (Button button in ActiveList)
            {
               button.gameObject.SetActive(false);
            }
            foreach(Button button in InactiveList)
            {
                button.gameObject.SetActive(true);
            }
            if(toggle)
            {
                key = "Selection Player 2";
                toggle = false;
            }
            else
            {
                key = "Selection Player 1";
                toggle = true;
            }
           
        }

        public Sprite GetEuropeSprite() { return _europeSprite; }
        public Sprite GetAsiaSprite() { return _asiaSprite; }
        public Sprite GetAfricaSprite() { return _africaSprite; }
        public Sprite GetOceaniaSprite() { return _oceaniaSprite; }

        private void StoreSelection(string selectionKey)
        {
            print(key);
            PlayerPrefs.SetInt(selectionKey, (int)_selection);
        }

        public enum Selection
        {
            none = 0,
            europe = 1,
            Oceania = 2,
            Africa = 3,
            Asia = 4
        }
    }
}
