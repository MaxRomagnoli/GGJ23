using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float minDistanceToDie = -1f;
    private Vector3 _playerStartingPoint;

    // Start is called before the first frame update
    void Start()
    {
        _playerStartingPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null ) { return; }
        if(player.transform.position.y < minDistanceToDie) { Die(); }
    }

    private void Die()
    {
        player.transform.position = _playerStartingPoint;
    }
}
