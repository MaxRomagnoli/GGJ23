using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] private Vector3 movement = new Vector3(1f, 1f, 1f);
    [SerializeField] private float speed = 1f;
    private Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float _oscillation = Mathf.Cos(Time.deltaTime * speed / Mathf.PI); //  Oscill(speed * Time.deltaTime);
        // Vector3 _movedPosition = new Vector3(Mathf.Sin(movement.x) * speed * Time.deltaTime, Mathf.Sin(movement.y) * speed * Time.deltaTime, Mathf.Sin(movement.z) * speed * Time.deltaTime);
        this.transform.position = _initialPosition + movement * _oscillation;
    }

    /*private float Oscill(float time, float speed, float scale)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * scale;
    }*/
}
