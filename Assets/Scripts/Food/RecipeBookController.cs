using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookController : MonoBehaviour
{
    private bool _firstLook = true;

    private int _randomPage;

    private GameObject _currentPage;
    [SerializeField] private GameObject _background;
    [SerializeField] private List<GameObject> _blackedOutPages = new List<GameObject>();
    [SerializeField] private List<GameObject> _normalPages = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> _currentIngredientsList = new List<GameObject>();
    private void Start()
    {
        _randomPage = Random.Range(0, _normalPages.Count);
        _currentIngredientsList = _normalPages[_randomPage].GetComponent<RecipeHolder>().IngredientList;
        _currentPage = _normalPages[_randomPage];
    }
    public void StartInteraction()
    {
        Time.timeScale = 0f;

        _background.SetActive(true);
        if (_firstLook)
        {
            _firstLook = false;

            _normalPages[_randomPage].SetActive(true);
            _currentPage = _normalPages[_randomPage];
        }
        else
        {
            _blackedOutPages[_randomPage].SetActive(true);
            _currentPage = _blackedOutPages[_randomPage];
        }
    }
    public void StopInteraction()
    {
        Time.timeScale = 1.0f;

        _background.SetActive(false);
        _currentPage.SetActive(false);
    }
}
