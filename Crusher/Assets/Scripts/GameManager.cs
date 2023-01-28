using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ball Settings")]
    [SerializeField] private GameObject _ballPrefab;
    
    [Header("Ball Pooling Settings")]
    [SerializeField] private float _ballSpawnTime;
    [SerializeField] private GameObject _ballSpawnPoint;
    [SerializeField] private GameObject _ballParent;

    private float _timer = 0;

    private bool _throwBall = true;

    private List<GameObject> _listBall;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartMethod();
    }

    private void Update()
    {
        UpdateMethods();
    }

    #region CoreMethods

    private void StartMethod()
    {
        _listBall = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject newBall = Instantiate(_ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBall.transform.parent = _ballParent.transform;
            _listBall.Add(newBall);
            newBall.SetActive(false);
        }
    }

    private void UpdateMethods()
    {
        if (_throwBall)
        {
            _timer += Time.deltaTime;
            if (_timer >= _ballSpawnTime)
            {
                foreach (var item in _listBall)
                {
                    if (!item.activeInHierarchy)
                    {
                        item.transform.position = _ballSpawnPoint.transform.position;
                        item.SetActive(true);
                        break;
                    }
                }
                _throwBall = false;
                _timer = 0;
            }
        }
    }

    #endregion

    #region Other Methods

    public void CanThrowNewBall()
    {
        _throwBall = true;
    }

    #endregion

}
