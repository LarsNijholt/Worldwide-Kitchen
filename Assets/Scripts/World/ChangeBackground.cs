using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.World
{
    public class ChangeBackground : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backGround;
        [SerializeField] private Sprite _europeBG;
        [SerializeField] private Sprite _africaBG;
        [SerializeField] private Sprite _oceaniaBG;
        [SerializeField] private Sprite _asiaBG;
        [SerializeField] private Sprite _defaultBG;

        /// <summary>
        /// Switches out the background based on where the player is
        /// </summary>
        public void UpdateBackGround(int backGround)
        {
            switch (backGround)
            {
                case 0:
                    {
                        _backGround.sprite = _europeBG;
                        break;
                    }

                case 1:
                    {
                        _backGround.sprite = _africaBG;
                        break;
                    }

                case 2:
                    {
                        _backGround.sprite = _oceaniaBG;
                        break;
                    }

                case 3:
                    {
                        _backGround.sprite= _asiaBG;
                        break;
                    }

                case 4:
                    {
                        _backGround.sprite = _defaultBG;
                        break;
                    }
            }
        }
    }
}
