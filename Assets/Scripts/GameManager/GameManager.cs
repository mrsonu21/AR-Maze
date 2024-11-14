using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _bestTimerText;
    [SerializeField] private TextMeshProUGUI __countdownText;
    [SerializeField] private GameObject _menuUI; 
    [SerializeField] private int _countdownTimerMax = 4;
    [Header("Finish Sequence")]
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private TextMeshProUGUI _finishBestScore;
    [SerializeField] private TextMeshProUGUI _finishYourScore;

    private int _countdownTimer;
    private float _timer;

    private bool _isFinished = false;
  

    private bool _isStarting = false;

    public static Action<bool> IsGameStarted;

    private void OnEnable()
    {
        PlayerMovement.IsOnFinish += EndSequence;
        MazeGenerator.OnGameStarted += StartSequence;
    }

    private void OnDisable()
    {
        PlayerMovement.IsOnFinish -= EndSequence;
        MazeGenerator.OnGameStarted -= StartSequence;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStarting)
        {
            Timer();
        }

        PauseGame();

    }

    private  void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _countdownTimer <= 1 && _isFinished == false)
        {
            Debug.Log("Pause Key ");
            PauseGameButton();
        }

    }
    public void PauseGameButton()
    {
        if (_countdownTimer <= 1 && _isFinished == false)
        {
            Debug.Log("Pause Button ");
            _menuUI.SetActive(!_menuUI.activeInHierarchy);
            _isStarting = false;
            
            _playerMovement.PauseGame();
            if (_menuUI.activeInHierarchy == false)
            {
                StartCoroutine(OnCountdown());
            }
        }
    }

    private void StartSequence()
    {
        Debug.Log("Start Sequence ");
        __countdownText.enabled = true;
       
        StartCoroutine(OnCountdown());



    }

    private void EndSequence()
    {
        Debug.Log(" End sequence");
        _isStarting = false;
        _isFinished = true;
        SaveGame();
        _finishMenu.SetActive(true);
        _finishYourScore.text = "YourScore:" + _timer.ToString();
        _finishBestScore.text = "BestScore:" + PlayerPrefs.GetFloat("time").ToString();

    }

    private IEnumerator OnCountdown()
    {
        _countdownTimer = _countdownTimerMax;
        __countdownText.enabled = true;
        _playerPrefab.SetActive(true);
        while (_countdownTimer >1)
        {
            _countdownTimer--;
            __countdownText.text = _countdownTimer.ToString();
            yield return new WaitForSeconds(1f);
        }

        if(_countdownTimer <= 1)
        {
            __countdownText.enabled = false;
            _isStarting = true;
            _playerMovement.StartGameSequence();
        }


    }

    private void Timer()
    {
     
        _timer += 1 * Time.deltaTime;
        _timerText.text = "Time:" + _timer.ToString();

    }

    private void SaveGame()
    {

        if (_timer < PlayerPrefs.GetFloat("time") || PlayerPrefs.GetFloat("time") == 0)
        {

            Debug.Log(" Save Game");
            PlayerPrefs.SetFloat("time", _timer);
        }
        
    }

    private void LoadGame()
    {
        Debug.Log("Load Game ");
        _bestTimerText.text = "BestTime: " +PlayerPrefs.GetFloat("time").ToString();
    }


}
