using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private float minDistanceToDie = -1f;
    private Vector3 _playerStartingPoint;

    [Header("Items")]
    [SerializeField] private Text itemsTakenText;
    [SerializeField] private GameObject itemsParent;
    private List<GameObject> _itemsToCollect;
    private int _takenTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Player
        _playerStartingPoint = player.transform.position;

        // Items
        _itemsToCollect = new List<GameObject>();
        foreach (Transform _transform in itemsParent.transform)
        {
            GameObject _obj = _transform.gameObject;
            int _taken = PlayerPrefs.GetInt(_obj.name, 0);
            if(_taken > 0) { SetAsTekenItem(_obj); }
            else { _itemsToCollect.Add(_obj); }
        }

        UpdateText();
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

    // Items

    public void CheckItem(GameObject _obj)
    {
        foreach (GameObject _item in _itemsToCollect)
        //for (int i = 0; i < _itemsToCollect.Count(); i++)
        {
            if(_item == null) { continue; }
            if (_obj.name == _item.name)
            { SetAsTekenItem(_obj); return; }
        }

        UpdateText();
    }

    private void SetAsTekenItem(GameObject _obj, int _value = 1)
    {
        PlayerPrefs.SetInt(_obj.name.ToString(), 1);
        _itemsToCollect.Remove(_obj);
        Destroy(_obj);
        _takenTotal += _value;
    }

    private void UpdateText()
    {
        itemsTakenText.text = _takenTotal.ToString();
    }
}
