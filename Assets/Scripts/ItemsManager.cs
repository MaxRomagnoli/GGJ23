using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.CheckItem(other.gameObject);
    }

}
