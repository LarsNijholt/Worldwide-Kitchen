using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class KitchenDoorController : MonoBehaviour
{
    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;
    [SerializeField] private GameObject _linkedDoor;
    [SerializeField] private GameObject _interactPopup;
    [SerializeField] private GameObject _fadeUI;

    private Animator _animator;

    private int _collided;

    private bool _faded;
    private bool _inRange;

    private void Awake()
    {
        _animator = _fadeUI.GetComponent<Animator>();
    }
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
    private void ShowPopup() { _interactPopup.SetActive(true); _inRange = true; }
    private void HidePopup() { _interactPopup.SetActive(false); _inRange = false; }
    private void Update()
    {
        if (_inRange && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        if (_faded == false)
            _animator.SetBool("Faded", true);
        else
            _animator.SetBool("Faded", false);

        if (_faded == false)
        {
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);

            Vector2 _doorPos = _linkedDoor.transform.position;

            _player1.transform.position = new Vector2(_doorPos.x - 1.5f, _doorPos.y);
            _player2.transform.position = new Vector2(_doorPos.x + 1.5f, _doorPos.y);

            _faded = true;
            StartCoroutine(Fade());
        }
        else _faded = false;
    }
}
