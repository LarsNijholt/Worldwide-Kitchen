using Assets.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Player1 : BaseCharacter
    {
        void Update()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            MoveSideways(this.gameObject, KeyCode.A, KeyCode.D);
            Jump(this.gameObject, KeyCode.W);
        }
    }
}
