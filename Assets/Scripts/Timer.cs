using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chowen
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] public static float timeRemaining = 13f;
        public static bool startGameCountdown = false;
        private TextMeshProUGUI timerText;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private TextMeshProUGUI startCountdownTimer;

        private void Start()
        {
            startGameCountdown = false;
            timerText = GetComponent<TextMeshProUGUI>();
            timeRemaining = 13f;
        }
        private void Update()
        {
            TimerSound();

            if (timeRemaining > 0 && GameManager.isGameActive)
            {
                timeRemaining -= Time.deltaTime;

                if (timeRemaining > 10)
                {
                    if (!startGameCountdown)
                    {
                        //this is the start countdown timer
                        startCountdownTimer.text = (timeRemaining - 10).ToString("0.");
                        InitialCoundownTimer();
                    }
                    else
                    {
                        startGameCountdown = true;
                        timerText.text = timeRemaining.ToString("00.00");
                    }
                }
                else
                {
                    //this is the game timer
                    timerText.text = timeRemaining.ToString("00.00");
                    //this is the start countdown timer
                    startCountdownTimer.text = "";
                    startGameCountdown = true;
                }
                
            }
            else
            {
                EventManager.GameOver?.Invoke();
            }

            //if (GameManager.isGameActive == true)
            //{
            //    TimerSound();
            //}
        }

        private void TimerSound()
        {
            if (timeRemaining <= 3.1 && timeRemaining >= 3)
            {
                audioManager.Play("3");
                Debug.Log("3");
            }
            else if (timeRemaining <= 2.1 && timeRemaining >= 2)
            {
                audioManager.Play("2");
            }
            else if (timeRemaining <= 1.1 && timeRemaining >= 1)
            {
                audioManager.Play("1");
            }
            else if (timeRemaining <= 0.001 && timeRemaining >= 0)
            {
                audioManager.Play("0");
            }
        }

        private void InitialCoundownTimer()
        {
            if (timeRemaining <= 13 && timeRemaining >= 12.99 | timeRemaining <= 12 && timeRemaining >= 11.99 | timeRemaining <= 11 && timeRemaining >= 10.99)
            {
                audioManager.Play("Tick");
                Debug.Log("Tick");
            }
            else if (timeRemaining <= 10.01 && timeRemaining >= 10)
            {
                audioManager.Play("Tick2");
                Debug.Log("Tick2");
            }
        }
        private void AddTime(float time)
        {
            timeRemaining += time;
        }

        private void SubtractTime(float time)
        {
            timeRemaining -= time;
        }

        private void OnEnable()
        {
            EventManager.HeartEaten += AddTime;
            EventManager.BadPelletEaten += SubtractTime;
        }
        private void OnDisable()
        {
            EventManager.HeartEaten -= AddTime;
            EventManager.BadPelletEaten -= SubtractTime;
        }
    }
}