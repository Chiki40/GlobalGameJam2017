using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmYourFather : MonoBehaviour
{
    private Dictionary<int, Transform> m_originalTransforms;

    void Awake()
    {
        m_originalTransforms = new Dictionary<int, Transform>();
    }


    void OnCollisionEnter(Collision collision)
    {
        YouBelongToMe(collision.gameObject);
    }
    void OnCollisionExit(Collision collision)
    {
        YouAreFree(collision.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        YouBelongToMe(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        YouAreFree(other.gameObject);
    }

    private void YouBelongToMe(GameObject go)
    {
        m_originalTransforms.Add(go.GetInstanceID(), go.transform.parent);
        go.transform.parent = this.transform;
    }

    private void YouAreFree(GameObject go)
    {
        go.transform.parent = m_originalTransforms[go.GetInstanceID()];
        m_originalTransforms.Remove(go.GetInstanceID());
        
    }
}
