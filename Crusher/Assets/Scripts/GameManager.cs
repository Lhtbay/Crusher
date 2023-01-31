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

    [Header("Crush Wall Pooling Settings")]
    [SerializeField] private GameObject _crushWallPrefab;
    [SerializeField] private GameObject _crushWallParent;
    private List<GameObject> _listCrushWall;
    private float _notActiveCrushWallCount, _ratioActiveNotActiveCount, _crushListCount = 0;

    [Header("Pusher Treadmill Settings")]
    [SerializeField] private GameObject _pusherPrefab;
    [SerializeField] private GameObject _pusherParent;
    [SerializeField] private GameObject _pusherStartPoint;
    [SerializeField] private float _pusherBeSpawnTime;
    private List<GameObject> _listPusher;

    private float _timer,_timerPusherSpawn,_timer2 = 0;
    private float _crushWallPositionX = 3f;
    private float _crushWallPositionY = 5f;
    private float _crushWallPositionZ = -0.5f;

    private bool _throwBall = true;

    private List<GameObject> _listBall;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        print(" !!! This game made in just 2 days !!! Baþer Alper Yalnýz");
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
        _listPusher = new List<GameObject>();
        _listCrushWall = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            GameObject newBall = Instantiate(_ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newBall.transform.parent = _ballParent.transform;
            _listBall.Add(newBall);
            newBall.SetActive(false);
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject newCrush = Instantiate(_crushWallPrefab,
                    new Vector3(_crushWallPositionX, _crushWallPositionY, _crushWallPositionZ),Quaternion.identity);
                newCrush.transform.parent = _crushWallParent.transform;
                _listCrushWall.Add(newCrush);
                _crushWallPositionY -= 0.5f;
            }
            _crushWallPositionX += 0.5f;
            _crushWallPositionY = 5f;
        }

        _crushListCount = _listCrushWall.Count;

        for (int i = 0; i < 10; i++)
        {
            GameObject newPusher = Instantiate(_pusherPrefab, _pusherStartPoint.transform.position,Quaternion.identity);
            newPusher.SetActive(false);
            newPusher.transform.parent = _pusherParent.transform;
            _listPusher.Add(newPusher);
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

        _timerPusherSpawn += Time.deltaTime;
        if (_timerPusherSpawn >= _pusherBeSpawnTime)
        {
            foreach (var item in _listPusher)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = _pusherStartPoint.transform.position;
                    item.SetActive(true);
                    break;
                }
            }
            _timerPusherSpawn = 0;
        }

        _timer2 += Time.deltaTime;
        if (_timer2 >= 5)
        {
            CheckGameOver();
            _timer2 = 0;
        }

    }

    #endregion

    #region Other Methods

    public void CanThrowNewBall()
    {
        _throwBall = true;
    }

    private void CheckGameOver()
    {
        _notActiveCrushWallCount = 0;
        foreach (var item in _listCrushWall)
        {
            if (!item.activeInHierarchy)
            {
                _notActiveCrushWallCount++;
            }
        }
        
        _ratioActiveNotActiveCount = _notActiveCrushWallCount / _crushListCount;              

        if (_ratioActiveNotActiveCount >= 0.8f)
        {
            print("YOU ARE WÝNNER");
        }

    }

    #endregion

}
