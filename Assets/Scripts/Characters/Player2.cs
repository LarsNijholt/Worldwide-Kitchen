using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Player2 : BaseCharacter
    {
        private KeyCode jump = KeyCode.W;

        private void Start()
        {
            SwitchCharacter(this, "Selection Player 2");   
        }

        void Update()
        {
            UpdateMovement(jump, "Horizontal2", "Vertical2");
        }

        private void FixedUpdate()
        {
            FixedMovement(jump);
        }

    }
}
