using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillCrushWall : MonoBehaviour
{
    public bool IsTouchBall = false;

    [Header("Crush Wall Settings")]
    [SerializeField] private float _explosionPower;
 
    private Rigidbody _touchCrushWallRb,_thisRb;

    private void Start()
    {
        _thisRb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.tag == "Wall" && IsTouchBall)
        {           
            _touchCrushWallRb = collision.gameObject.GetComponent<Rigidbody>();
            if (_touchCrushWallRb.isKinematic)
            {               
                _touchCrushWallRb.isKinematic = false;
                _touchCrushWallRb.AddForce(Vector3.left*_explosionPower);          
                IsTouchBall = false;
            }                     
        }
        if (collision.gameObject.tag == "Money")
        {
            this.gameObject.SetActive(false);
        }
    }

}
