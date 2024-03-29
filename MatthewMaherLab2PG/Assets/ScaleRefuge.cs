using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRefuge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale /= 2;
        transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
