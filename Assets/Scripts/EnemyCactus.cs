using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCactus : MonoBehaviour
{
    public GameObject Cactus;

    private const float RespDelay = 2f;
    private Transform _transform;
    private float tiempo = 0f;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 5)
        {
            Instantiate(Cactus, _transform.position, Quaternion.identity);
            tiempo = 0;

        }
        if (tiempo == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
