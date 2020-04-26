using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaWatcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsInZone;

    public List<GameObject> ObjectsInZone => _objectsInZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Squad>() != null)
        {
            _objectsInZone.Add(collision.gameObject);
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _objectsInZone.Remove(collision.gameObject);
    }
}
