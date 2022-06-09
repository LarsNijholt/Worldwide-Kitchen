using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public void OpenDoor()
    {
        _anim.SetBool("Open", true);
    }
    public void CloseDoor()
    {   
        _anim.SetBool("Open", false);
    }
}
