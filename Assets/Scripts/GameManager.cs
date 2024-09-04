using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private AudioSource audioSource;
    public static GameManager Instance { get; set; }
    enum State
    {
        waitingtostart,
        countdowntostart,
        gameplaying,
        gameover,
    }
    State state;
    public float waitingtostarttimer = 1;
    public float countdowntostarttimer = 3;
    public float gameplayingtimer;
    private bool hasPlayedAudio = false; // Flag to ensure the audio plays only once

    void Awake()
    {
        state = State.waitingtostart;
    }
    void Start()
    {
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
                    timerGUI.SetActive(false);
                    state = State.gameover;
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
    public void TriggerAddTime()
    {
        addtime?.Invoke();
    }
    public void TriggerTakeTime()
    {
        takefromtime?.Invoke();
    }
}
