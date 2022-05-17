using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class SpeedCharacter : BaseCharacter
    {
        void Update()
        {
            MoveCharacter(this.gameObject);
        }
    }
}
