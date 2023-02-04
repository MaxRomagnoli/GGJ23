using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private float minDistanceToDie = -1f;
    private ThirdPersonController _thirdPersonController;
    private Vector3 _playerStartingPoint;

    [Header("Items")]
    [SerializeField] private TMP_Text itemsTakenText;
    [SerializeField] private GameObject itemsParent;
    [SerializeField] private int itemsToJump = 10;
    [SerializeField] private int itemsToRun = 20;
    private List<GameObject> _itemsToCollect;
    private string _textBeforeTaken;
    private int _takenTotal = 0;

    /*[Header("Menu")]
    [SerializeField] private GameObject mainMenu;*/

    // Start is called before the first frame update
    void Start()
    {
        // Player
        _playerStartingPoint = player.transform.position;
        _thirdPersonController = player.GetComponent<ThirdPersonController>();
        _thirdPersonController.SetCanJump(false);
        _thirdPersonController.SetCanRun(false);

        // Items
        _itemsToCollect = new List<GameObject>();
        _textBeforeTaken = itemsTakenText.text;
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
        // Player
        if(player.transform.position.y < minDistanceToDie) { Die(); }

        // Menu
        /*if(Input.GetKeyDown("KeyCode.Escape") || Input.GetKeyDown("Fire3"))
        {
            ToogleMenu();
        }*/
    }

    private void Die()
    {
        player.transform.position = _playerStartingPoint;
    }

    // Items

    public void CheckItem(GameObject _obj)
    {
        foreach (GameObject _item in _itemsToCollect)
        {
            if(_item == null) { continue; }
            if (_obj.name == _item.name)
            {
                SetAsTekenItem(_obj);
                UpdateText();
                return;
            }
        }
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
        itemsTakenText.text = _textBeforeTaken + _takenTotal.ToString();
        if(_takenTotal >= itemsToJump) { _thirdPersonController.SetCanJump(); }
        if(_takenTotal >= itemsToRun) { _thirdPersonController.SetCanRun(); }
    }

    // Menu

    /*private void ToogleMenu()
    {
        ToogleMenu(!mainMenu.active);
    }

    private void ToogleMenu(bool _active)
    {
        mainMenu.SetActive(_active);
        if(_active)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }*/
}
