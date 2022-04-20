using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class SpeedCharacter : BaseCharacter
    {
        void Start()
        {
            
        }

        void Update()
        {
            MoveSideways(this.gameObject);
        }
    } 
}
