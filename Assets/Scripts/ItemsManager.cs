using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToCollect;
    [SerializeField] private Text itemsTakenText;
    private int _takenTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in itemsToCollect)
        {
            int _taken = PlayerPrefs.GetInt(go.name, 0);
            if(_taken > 0)
            {
                _takenTotal += _taken;
                Destroy(go);
            }
        }
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject _item in itemsToCollect)
        {
            if (other.name == _item.name)
            {
                // SetAsTaken
                PlayerPrefs.SetInt(other.name.ToString(), 1);
                Destroy(other.gameObject);
                _takenTotal += 1;

                UpdateText();
            }
        }
    }

    private void UpdateText()
    {
        itemsTakenText.text = _takenTotal.ToString();
    }

}
