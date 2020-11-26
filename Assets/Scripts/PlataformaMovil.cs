using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform target;
    private const float speed = 1;

    private Vector3 start, end;
    void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    void FixedUpdate()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (transform.position == target.position)
            target.position = (target.position == start) ? end : start;
    }
}
