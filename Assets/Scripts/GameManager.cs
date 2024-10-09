using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    [SerializeField] Button[] RewardButtons = new Button[2];
    public event Action addtime;
    public event Action takefromtime;
    [SerializeField] float reaored = 12;
    [SerializeField] TextMeshProUGUI countingdounwUI;
    [SerializeField] GameObject countGUI;
    [SerializeField] TextMeshProUGUI gameplaytimeUI;
    [SerializeField] GameObject timerGUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject gamepad;
    [SerializeField] AudioClip timerunnigout;
    [SerializeField] GameObject gamemenu;
    public AudioSource audioSource;
    
    public enum State
    {
        waitingtostart,
        countdowntostart,
        gameplaying,
        gameover,
    }
    public State state;
    [SerializeField] State PrevState;

    public float waitingtostarttimer = 1;
    public float countdowntostarttimer = 3;
    public float gameplayingtimer;
    public bool hasPlayedAudio = false; // Flag to ensure the audio plays only once

    void Awake()
    {
        
        state = State.waitingtostart;
    }
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the GameObject.");
            return;
        }

        timerGUI.SetActive(false);
        countGUI.SetActive(true);
        gameoverUI.SetActive(false);
        addtime += addreaored;
        takefromtime += taketime;
    }
    void Update()
    {
        if(PrevState != state)
        {
            PrevState = state;
        }
        switch (state)
        {
            case State.waitingtostart:
                Time.timeScale = 1f;
                waitingtostarttimer -= Time.deltaTime;
                if (waitingtostarttimer < 0f)
                {
                    state = State.countdowntostart;
                }
                break;

            case State.countdowntostart:
                countdowntostarttimer -= Time.deltaTime;
                countingdounwUI.text = ((int)countdowntostarttimer).ToString();
                if (countdowntostarttimer < 0f)
                {
                    countGUI.SetActive(false);
                    state = State.gameplaying;
                }
                break;

            case State.gameplaying:
                timerGUI.SetActive(true);
                gamepad.SetActive(Application.isMobilePlatform);
                gameplaytimeUI.text = ((int)gameplayingtimer).ToString();
                gameplayingtimer -= Time.deltaTime;

                if (gameplayingtimer <= 10 && !hasPlayedAudio)
                {
                    gameplaytimeUI.GetComponent<Animator>().SetTrigger("dangeur");
                    audioSource.clip = timerunnigout;
                    audioSource.Play();
                    hasPlayedAudio = true; // Set the flag to true to avoid replaying
                }
                else if (gameplayingtimer < 0f)
                {
                    audioSource.Stop();
                    timerGUI.SetActive(false);
                    state = State.gameover;
                }
                else if(gameplayingtimer > 10)
                {
                    hasPlayedAudio = false;
                    gameplaytimeUI.GetComponent<Animator>().SetTrigger("back");
                    audioSource.Stop();
                }

                break;

            case State.gameover:
                audioSource.Stop();
                gameoverUI.SetActive(true);
                gamepad.SetActive(false);
                gamemenu.SetActive(false);
                Time.timeScale = 0f;
                break;
        }
        
    }
    void addreaored()
    {
        gameplayingtimer += reaored;
        if (reaored > 7)
        {
            reaored--;
        }
    }
    void taketime()
    {
        gameplayingtimer -= 10;
    }
    public void addtimereward()
    {
        gameplaytimeUI.GetComponent<Animator>().SetTrigger("back");
        gameplayingtimer += 21;
        gamemenu.SetActive(true);
        Time.timeScale = 0;
        state = PrevState;
        RewardButtons[0].interactable = false;
        RewardButtons[1].interactable = false;

    }
    public void TriggerAddTime()
    {
        addtime?.Invoke();
    }
    public void TriggerTakeTime()
    {
        takefromtime?.Invoke();
    }
}