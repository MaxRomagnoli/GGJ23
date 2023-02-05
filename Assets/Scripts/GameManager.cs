using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
//using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cylinder;
    [SerializeField] private float minDistanceToDie = -1f;
    [SerializeField] private Vector2 gravityValues = new Vector2(-15f, -5f);
    [SerializeField] private float jetpackForce = 100f;
    [SerializeField] private float jetpackRotation = 50f;
    [SerializeField] private GameObject jetpack;
    [SerializeField] private GameObject glasses;
    private ThirdPersonController _thirdPersonController;
    private SphereCollider _sphereCollider;
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private Rigidbody _rb;
    private Vector3 _playerStartingPoint;
    private bool _gameOver = false;
    
    [Header("Camera")]
    [SerializeField] private GameObject followCamera;
    [SerializeField] private GameObject lookAtCamera;
    //private CinemachineVirtualCamera _camera;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Slider itemsTakenSlider;
    [SerializeField] private Image jumpImage;
    [SerializeField] private Image runImage;
    [SerializeField] private Color colorWhenAchived;

    [Header("Items")]
    [SerializeField] private GameObject itemsParent;
    [SerializeField] private int itemsToJump = 10;
    [SerializeField] private int itemsToRun = 20;
    private List<GameObject> _itemsToCollect;
    private int _takenTotal = 0;

    /*[Header("Menu")]
    [SerializeField] private GameObject mainMenu;*/

    // Start is called before the first frame update
    void Start()
    {
        // Camera 
        gameOverPanel.SetActive(false);
        followCamera.SetActive(true);
        lookAtCamera.SetActive(false);

        // Player
        _playerStartingPoint = player.transform.position;
        _rb = player.GetComponent<Rigidbody>();
        _input = player.GetComponent<StarterAssetsInputs>();
        _sphereCollider = player.GetComponent<SphereCollider>();
        _controller = player.GetComponent<CharacterController>();
        _thirdPersonController = player.GetComponent<ThirdPersonController>();
        _thirdPersonController.SetCanJump(false);
        _thirdPersonController.SetCanRun(false);

        // Items
        _itemsToCollect = new List<GameObject>();
        itemsTakenSlider.maxValue = 0;
        foreach (Transform _transform in itemsParent.transform)
        {
            itemsTakenSlider.maxValue += 1;
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
        if(!_gameOver && player.transform.position.y < minDistanceToDie) { Die(); }

        // Menu
        /*if(Input.GetKeyDown("KeyCode.Escape") || Input.GetKeyDown("Fire3"))
        {
            ToogleMenu();
        }*/
    }

    void FixedUpdate()
    {
        if(_gameOver)
        {
            if(_input.move != Vector2.zero)
            {
                _rb.AddRelativeTorque(_input.move * jetpackRotation * Time.fixedDeltaTime, ForceMode.Acceleration);
            }

            if(_input.look != Vector2.zero)
            {
                _rb.AddForce(_input.look * Time.fixedDeltaTime * jetpackForce, ForceMode.Acceleration);
            }
        }
    }

    private void Die()
    {
        Debug.Log("Died");
        _thirdPersonController.enabled = false;
        player.transform.position = _playerStartingPoint;
        _thirdPersonController.enabled = true;
    }

    // Items

    public void CheckItem(GameObject _obj)
    {
        if(_obj.tag == "gameover") { GameOver(); }
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
        itemsTakenSlider.value = _takenTotal;

        if(_takenTotal >= itemsToJump) {
            _thirdPersonController.SetCanJump();
            jumpImage.color = colorWhenAchived;
        }

        if(_takenTotal >= itemsToRun) {
            _thirdPersonController.SetCanRun();
            runImage.color = colorWhenAchived;
        }

        // (old_value - old_min) * (new_max - new_min) / (old_max - old_min) + new_min;
        float _result = (_takenTotal - 0) * (gravityValues.y - gravityValues.x) / (itemsTakenSlider.maxValue - 0) + gravityValues.x;
        _thirdPersonController.SetGravity(_result);
    }

    public void GameOver()
    {
        _gameOver = true;
        _thirdPersonController.enabled = false;
        _controller.enabled = false;
        followCamera.SetActive(false);
        lookAtCamera.SetActive(true);
        StartCoroutine(IntoTheCilinder());
        //_camera.m_LookAt = player.transform;
        //_camera.m_Follow = null;
    }

    private IEnumerator IntoTheCilinder()
    {
        float elapsedTime = 0;
        float waitTime = 2f;
        Vector3 currentPos = player.transform.position;
        Vector3 cylinderPos = cylinder.transform.position - (Vector3.up * 1f);
        while (elapsedTime < waitTime)
        {
            player.transform.position = Vector3.Lerp(currentPos, cylinderPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }  
        
        cylinder.SetActive(false);
        jetpack.SetActive(true);
        glasses.SetActive(true);
        _rb.isKinematic = false;
        _sphereCollider.enabled = true;
        
        elapsedTime = 0;
        waitTime = 5f;
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        gameOverPanel.SetActive(true);
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
