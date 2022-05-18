using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Player2 : BaseCharacter
    {
        void Update()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            MoveSideways(this.gameObject, KeyCode.LeftArrow, KeyCode.RightArrow);
            Jump(this.gameObject, KeyCode.UpArrow);
        }
    } 
}
