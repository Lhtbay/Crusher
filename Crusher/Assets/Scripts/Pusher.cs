using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] private float _speed;    
    private GameObject _leftFinishPoint;

    private void Start()
    {
        _leftFinishPoint = GameObject.FindGameObjectWithTag("LeftFinish");
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position,_leftFinishPoint.transform.position)>0.2f)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }



}
