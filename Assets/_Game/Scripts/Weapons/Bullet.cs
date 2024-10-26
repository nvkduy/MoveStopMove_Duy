using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] GameObject bulletVisual;
    private float maxDistance = 2.1f;
    private Vector3 startBullet;
    
    private void Start()
    {
        startBullet = transform.position;
    }
    void Update()
    {
       bulletVisual.transform.Rotate(new Vector3(0,0,rotationSpeed));
        float distanceTravelled = Vector3.Distance(startBullet, transform.position);
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            PlayerManager.Instance.KillNumber();
            
        }
    }



}
