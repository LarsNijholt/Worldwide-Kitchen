using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private DoorController _doorToOpen;
    
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) ButtonDown();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) ButtonUp();
    }

    private void ButtonDown()
    {
        _renderer.enabled = false;
        _doorToOpen.StopAllCoroutines();
        _doorToOpen.StartCoroutine(_doorToOpen.OpenDoor());
    }

    private void ButtonUp()
    {
        _renderer.enabled = true;
        _doorToOpen.StopAllCoroutines();
        _doorToOpen.StartCoroutine(_doorToOpen.CloseDoor());
    }
}
