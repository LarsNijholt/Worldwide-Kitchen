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

    [SerializeField] private GameObject _fadeUI;

    private Animator _animator;
    private CameraController _cameraController;

    private bool _faded;
    private bool _insideKitchen;

    private Vector3 _insideCamPos = new Vector3(179.5f, 5.43f, -10);

    private void Awake()
    {
        _animator = _fadeUI.GetComponent<Animator>();
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void InteractDoor() { StartCoroutine(Fade()); }

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
            if (_linkedDoor.CompareTag("InsideDoor")) InsideView();
            else OutsideView();
        }
        else _faded = false;
    }

    private void InsideView()
    {
        _cameraController.enabled = false;

        _cameraController.gameObject.transform.position = _insideCamPos;
        Camera.main.orthographicSize = 3.7f;
    }
    private void OutsideView()
    {
        Camera.main.orthographicSize = 7.001f;
        _cameraController.enabled = true;
    }
}
