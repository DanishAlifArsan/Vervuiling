using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float paralaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1-paralaxEffect));
        float distance = (camera.transform.position.x * paralaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        } else if(temp < startpos - length) {
            startpos -= length;
        }
    }
}
