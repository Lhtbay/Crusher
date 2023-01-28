using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _gravityPower;
    [SerializeField] private float _throwPower;
    [SerializeField] private float _destroyTime;

    private float _timer = 0;

    private bool _isTouchLine, _canOneTimeRun = false;

    private Rigidbody _thisRb;

    private void Start()
    {
        StartMethod();
    }

    private void Update()
    {
        UpdateMethod();       
    }

    #region CoreMethods

    private void StartMethod()
    {
        _thisRb = this.GetComponent<Rigidbody>();
    }

    private void UpdateMethod()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            _timer = 0;           
            this.gameObject.SetActive(false);
        }
        _canOneTimeRun = true;
    }

    private void OnDisable()
    {
        if (_canOneTimeRun)
        {
            GameManager.Instance.CanThrowNewBall();
            _thisRb.velocity = Vector3.zero;
            _isTouchLine = false;
            _canOneTimeRun = false;
        }        
    }

    #endregion

    #region Others Methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Line" && !_isTouchLine)
        {            
            _isTouchLine = true;
            _thisRb.velocity = Vector3.zero;
            _thisRb.AddForce(Vector3.right*_throwPower);
        }
    }

    #endregion


}
