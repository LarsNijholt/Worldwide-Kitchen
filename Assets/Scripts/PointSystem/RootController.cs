using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    [HideInInspector] public int toolbarInt;
    [HideInInspector] public bool loop;
    [HideInInspector] public bool pingpong;
    [HideInInspector] public bool oneway;

    private void OnValidate()
    {
        
    }

    public void ValidateEditor()
    {
        OnValidate();
    }
}
