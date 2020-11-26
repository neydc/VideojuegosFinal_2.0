using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVida : MonoBehaviour
{
    public GameObject[] Vida;
    private int life;

    void Start()
    {
        life = Vida.Length;
    }

    // Update is called once per frame
    public bool GetVida()
    {
        if (life < 1)
        {
            return false;
        }
        return true;
    }
    public void RemoveLife(int vida)
    {
        if (life >= 1)
        {
            life -= vida;
            Destroy(Vida[life].gameObject);
        }
    }
}
