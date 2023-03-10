using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private string ownerTag = "Player";
    [SerializeField] private string oppositeTag = "Enemy";
    
    void Start()
    {
        Invoke("SelfDestroy",2);
    }

    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == oppositeTag)
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
