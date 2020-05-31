using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    float destroyMoment;
    void Start()
    {
        destroyMoment = Time.time + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= destroyMoment)
            Destroy(gameObject);
    }
}
