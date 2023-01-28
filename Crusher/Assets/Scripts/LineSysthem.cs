using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSysthem : MonoBehaviour
{

    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _lineParent;

    [Header("Liner Settings")]
    [SerializeField] private float _drawLineDistance;
    [SerializeField] private int _mousePositionZ;

    private GameObject _currentLineObject;

    private bool _ýnstantiateNewLine,_firstClickMouse,_saveLastPosition = true;

    private Vector3 _mouseLastPosition;
    private Vector3 _mousePosition;
    private Vector3 _mouseCurrentPosition;

    private List<GameObject> _listLineObjects;

    private void Start()
    {
        StartMethod();
    }

    void Update()
    {
        MouseController();
    }

    #region Core Methods

    private void StartMethod()
    {
        _listLineObjects = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject newLine = Instantiate(_linePrefab,new Vector3 (0,0,0),Quaternion.identity);
            newLine.transform.parent = _lineParent.transform;
            _listLineObjects.Add(newLine);        
            newLine.SetActive(false);
        }

    }


    #endregion

    #region Control Methods

    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RemoveBeforeLine();
            _firstClickMouse = true;
            _ýnstantiateNewLine = true;
        }

        if (Input.GetMouseButton(0))
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = _mousePositionZ;
            _mouseCurrentPosition = Camera.main.ScreenToWorldPoint(_mousePosition);

            SaveLastPosition();
            _ýnstantiateNewLine = true;

            if (Vector3.Distance(_mouseLastPosition, _mouseCurrentPosition) >= _drawLineDistance || _firstClickMouse)
            {
                foreach (var item in _listLineObjects)
                {
                    if (!item.activeInHierarchy)
                    {
                        _ýnstantiateNewLine = false;
                        _currentLineObject = item;
                        item.SetActive(true);
                        
                        _saveLastPosition = true;
                        item.transform.position = _mouseCurrentPosition;

                        break;
                    }
                }
                              
                if (_ýnstantiateNewLine)
                {
                    GameObject newLine = Instantiate(_linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    newLine.transform.parent = _lineParent.transform;
                    _listLineObjects.Add(newLine);          
                    newLine.SetActive(true);

                    _currentLineObject = newLine;
                    _saveLastPosition = true;
                    newLine.transform.position = _mouseCurrentPosition;
                    _ýnstantiateNewLine = false;
                }
              
                _firstClickMouse = false;
            }

            //_currentLineObject.transform.LookAt(_mouseCurrentPosition);
            //_currentLineObject.transform.localScale = new Vector3(_currentLineObject.transform.localScale.x, _currentLineObject.transform.localScale.y, Vector3.Distance(_mouseLastPosition, _mouseCurrentPosition));
        }
    }

    private void RemoveBeforeLine()
    {
        foreach (var item in _listLineObjects)
        {
            item.SetActive(false);
        }
        
    }

    private void SaveLastPosition()
    {
        if (_saveLastPosition)
        {
            _mouseLastPosition = _mouseCurrentPosition;
            _saveLastPosition = false;
        }
    }

    #endregion

}
