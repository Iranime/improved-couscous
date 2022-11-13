using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExitPrison : MonoBehaviour
{
    public Text timerSecondsText;
    public Text leftPinText;
    public Text middlePinText;
    public Text rightPinText;
    public Text resultGameText;
    public Text timerSecondsTextResult;

    public GameObject resultPanel;
    public GameObject drillButton;
    public GameObject hammerButton;
    public GameObject picklockButton;
    public GameObject gamePanel;
    public GameObject menuPanel;

    public float maxTimeStart = 30f;
    public int leftPinNumberStart = 7;
    public int middlePinNumberStart = 3;
    public int rightPinNumberStart = 5;

    private float maxTime;
    private int leftPinNumber;
    private int middlePinNumber;
    private int rightPinNumber;
    private bool timeRunning = false;
    private int maxValue = 10;
    private int minValue;

    private void Inizialization()
    {
        maxTime = maxTimeStart;
        leftPinNumber = leftPinNumberStart;
        middlePinNumber = middlePinNumberStart;
        rightPinNumber = rightPinNumberStart;
        leftPinText.text = leftPinNumberStart.ToString();
        middlePinText.text = middlePinNumberStart.ToString();
        rightPinText.text = rightPinNumberStart.ToString();
        timerSecondsText.text = maxTimeStart.ToString();
    } //Инициализация необходимых переменных
    void Start()
    {
        Inizialization();
    } 
    public void StartGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        timeRunning = true;
    } //Переход из меню к геймплею + начало отсчета таймера
    void Update()
    {
        if (timeRunning)
        {
            maxTime -= Time.deltaTime;
            timerSecondsText.text = Mathf.Round(maxTime).ToString();
            PinsCheck();
            ResultGame();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        } //Выход из игры после нажатия клавиши esc (только через билд)
    }
    public void PinsCheck()
    {
        ButtonsOn();
        if (middlePinNumber == minValue)
        {
            drillButton.GetComponent<Button>().interactable = false;
            if (rightPinNumber == minValue) 
            {
                hammerButton.GetComponent <Button>().interactable = false;
            }
        }
        else if (leftPinNumber == minValue)
        {
            hammerButton.GetComponent<Button>().interactable = false;
            picklockButton.GetComponent<Button>().interactable = false;
        }
        else if (rightPinNumber == minValue)
        {
            hammerButton.GetComponent<Button>().interactable = false;
            if (leftPinNumber == maxValue) 
            {
                drillButton.GetComponent <Button>().interactable = false;
            }
        }
        else if (leftPinNumber == maxValue)
        {
            drillButton.GetComponent<Button>().interactable = false;
            if (rightPinNumber == maxValue)
            {
                picklockButton.GetComponent<Button>().interactable = false;
                if (middlePinNumber == maxValue- 1)
                {
                    hammerButton.GetComponent <Button>().interactable = false;
                }    
            }
            else if (middlePinNumber == maxValue)
            {
                hammerButton.GetComponent<Button>().interactable = false;
                picklockButton.GetComponent<Button>().interactable = false;
            }
            else if (middlePinNumber == maxValue - 1) 
            {
                hammerButton.GetComponent<Button>().interactable = false;
            }
        }
        else if (middlePinNumber == maxValue - 1)
        {
            hammerButton.GetComponent<Button>().interactable = false;
            if (rightPinNumber == maxValue)
            {
                picklockButton.GetComponent<Button>().interactable = false;
            }
        }
        else if (middlePinNumber == maxValue)
        {
            picklockButton.GetComponent<Button>().interactable = false;
            hammerButton.GetComponent<Button>().interactable = false;
            if (rightPinNumber == maxValue)
            {
                picklockButton.GetComponent<Button>().interactable = false;
            }
        }
        else if (rightPinNumber == maxValue)
        {
            picklockButton.GetComponent<Button>().interactable = false;
        }
    } //Проверям условия игры
    public void DrillButtonClick()
    {
        leftPinNumber += 1;
        middlePinNumber -= 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
    } //Результат нажатия кнопки с дрелью
    public void HammerButtonClick()
    {
        leftPinNumber -= 1;
        middlePinNumber += 2;
        rightPinNumber -= 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
        rightPinText.text = rightPinNumber.ToString();
    } // Результат нажатия кнопки с молотом
    public void PicklockButtonClick()
    {
        leftPinNumber -= 1;
        middlePinNumber += 1;
        rightPinNumber += 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
        rightPinText.text = rightPinNumber.ToString();
    } // Результат нажатия кнопки с отмычкой
    private void ButtonsOn()
    {
        drillButton.GetComponent<Button>().interactable = true;
        hammerButton.GetComponent<Button>().interactable = true;
        picklockButton.GetComponent<Button>().interactable = true;
    } //Включение всех кнопок
    private void ButtonsOff()
    {
        drillButton.GetComponent<Button>().interactable = false;
        hammerButton.GetComponent<Button>().interactable = false;
        picklockButton.GetComponent<Button>().interactable = false;
    } //Отключение всех кнопок
    public void Restart()
    {
        Inizialization();
        resultPanel.SetActive(false);
        timeRunning = true;
    } //Рестарт игры
    public void ResultGame()
    {
        if (maxTime <= 0 | (drillButton.GetComponent<Button>().interactable == false
            & hammerButton.GetComponent<Button>().interactable == false
            & picklockButton.GetComponent<Button>().interactable == false))
        {
            ButtonsOff();
            resultPanel.SetActive(true);
            resultGameText.text = "Escape failed, will you try again?";
            timeRunning = false;
        }
        else if (leftPinNumber == middlePinNumber & leftPinNumber == rightPinNumber)
        {
            ButtonsOff();
            resultPanel.SetActive(true);
            resultGameText.text = "The lock was opened, but the hero was quickly caught and sent back to the cell. Maybe better luck next time?";
            timeRunning = false;
        }
    } //Вывод экрана с исходом игры
    public void ExitGameButton()
    {
        Application.Quit();
    } //Выход из игры черещ кнопку в меню (только через билд)
}