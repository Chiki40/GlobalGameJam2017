using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public int ID;

    void OnCollisionEnter(Collision collision)
    {
        KeyPicked(collision.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        KeyPicked(other.gameObject);
    }
   
    private void KeyPicked(GameObject go )
    {
        LevelManager.GetInstance().KeyPicked(ID);

        Destroy(this.gameObject);
    }
}
