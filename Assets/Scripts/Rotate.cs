using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 speed = new Vector3(1f, 1f, 1f);
    [SerializeField] private bool random = false;
    [SerializeField] private Vector3 secondSpeed = new Vector3(2f, 2f, 2f);

    // Start is called before the first frame update
    void Start()
    {
        if(random) {
            speed = new Vector3(
                Random.Range(speed.x, secondSpeed.x),
                Random.Range(speed.y, secondSpeed.y),
                Random.Range(speed.z, secondSpeed.z)
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(speed * Time.deltaTime);
    }
}
