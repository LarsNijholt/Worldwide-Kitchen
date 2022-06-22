using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UI
{
    public class CharacterSelect : MonoBehaviour
    {
        [SerializeField] private Sprite _europeSprite;
        [SerializeField] private Sprite _oceaniaSprite;
        [SerializeField] private Sprite _africaSprite;
        [SerializeField] private Sprite _asiaSprite;
        public Selection _selection = Selection.none;

        public void SelectEurope() { _selection = Selection.europe; StoreSelection(); }

        public void SelectOceania() { _selection = Selection.Oceania; StoreSelection(); }

        public void SelectAfrica() { _selection = Selection.Africa; StoreSelection(); }

        public void SelectAsia() { _selection = Selection.Asia; StoreSelection(); }

        public Sprite GetEuropeSprite() { return _europeSprite; }
        public Sprite GetAsiaSprite() { return _asiaSprite; }
        public Sprite GetAfricaSprite() { return _africaSprite; }
        public Sprite GetOceaniaSprite() { return _oceaniaSprite; }

        private void StoreSelection()
        {
            PlayerPrefs.SetInt("Selection", (int)_selection);
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
