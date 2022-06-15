using Assets.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Player1 : BaseCharacter
    {
        private KeyCode jump = KeyCode.UpArrow;
        void Update()
        {
            UpdateMovement(jump, "Horizontal1", "Vertical1");
        }

        private void FixedUpdate()
        {
            FixedMovement(jump);
        }

    }
}
