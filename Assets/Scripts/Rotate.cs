using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 speed = new Vector3(1f, 1f, 1f);

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(speed * Time.deltaTime);
    }
}
