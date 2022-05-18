using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float _openHeight;
    [SerializeField] private float _closeHeight;
    private float _stepSize = 0.08f;
    private float _waitTime = 0.03f;
    public IEnumerator OpenDoor()
    {
        if (transform.localPosition.y <= _openHeight)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + _stepSize);
            yield return new WaitForSeconds(_waitTime);
            StartCoroutine(OpenDoor());
        }
        else
            transform.localPosition = new Vector2(transform.localPosition.x, _openHeight);
    }
    public IEnumerator CloseDoor()
    {   
        if (transform.localPosition.y >= _closeHeight)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - _stepSize);
            yield return new WaitForSeconds(_waitTime);
            StartCoroutine(CloseDoor());
        }
        else
            transform.localPosition = new Vector2(transform.localPosition.x, _closeHeight);
    }
}
