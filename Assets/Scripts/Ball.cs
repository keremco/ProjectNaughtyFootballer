using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float life = 2f;

    private void Awake()
    {
        Destroy(gameObject, life);

    }
}
