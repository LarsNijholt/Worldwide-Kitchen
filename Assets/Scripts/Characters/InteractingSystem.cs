using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractingSystem : MonoBehaviour
{
    [SerializeField] private GameObject _interactPopup;
    [SerializeField] private UnityEvent _startInteraction;
    [SerializeField] private UnityEvent _stopInteraction;


    private bool _canInteract;
    private bool _currentlyInteracting;

    private int _collided;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collided++;
            Invoke(_collided > 0 ? "ShowPopup" : "HidePopup", 0f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collided--;
            Invoke(_collided > 0 ? "ShowPopup" : "HidePopup", 0f);
        }
    }
    private void ShowPopup()
    {
        _interactPopup.SetActive(true);
        _canInteract = true;
    }
    private void HidePopup()
    {
        _interactPopup.SetActive(false);
        _canInteract = false;
        InteractionStopped();
    }

    private void Update()
    {
        if (!_canInteract) return;
        if (Input.GetKeyDown(KeyCode.Space))
            Invoke(_currentlyInteracting ? "InteractionStopped" : "InteractionStarted", 0f);
    }

    private void InteractionStarted()
    {
        _currentlyInteracting = true;
        _startInteraction.Invoke();
    }
    private void InteractionStopped()
    {
        _currentlyInteracting = false;
        _stopInteraction.Invoke();
    }
}
