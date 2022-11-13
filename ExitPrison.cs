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
    } //������������� ����������� ����������
    void Start()
    {
        Inizialization();
    } 
    public void StartGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        timeRunning = true;
    } //������� �� ���� � �������� + ������ ������� �������
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
        } //����� �� ���� ����� ������� ������� esc (������ ����� ����)
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
    } //�������� ������� ����
    public void DrillButtonClick()
    {
        leftPinNumber += 1;
        middlePinNumber -= 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
    } //��������� ������� ������ � ������
    public void HammerButtonClick()
    {
        leftPinNumber -= 1;
        middlePinNumber += 2;
        rightPinNumber -= 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
        rightPinText.text = rightPinNumber.ToString();
    } // ��������� ������� ������ � �������
    public void PicklockButtonClick()
    {
        leftPinNumber -= 1;
        middlePinNumber += 1;
        rightPinNumber += 1;
        leftPinText.text = leftPinNumber.ToString();
        middlePinText.text = middlePinNumber.ToString();
        rightPinText.text = rightPinNumber.ToString();
    } // ��������� ������� ������ � ��������
    private void ButtonsOn()
    {
        drillButton.GetComponent<Button>().interactable = true;
        hammerButton.GetComponent<Button>().interactable = true;
        picklockButton.GetComponent<Button>().interactable = true;
    } //��������� ���� ������
    private void ButtonsOff()
    {
        drillButton.GetComponent<Button>().interactable = false;
        hammerButton.GetComponent<Button>().interactable = false;
        picklockButton.GetComponent<Button>().interactable = false;
    } //���������� ���� ������
    public void Restart()
    {
        Inizialization();
        resultPanel.SetActive(false);
        timeRunning = true;
    } //������� ����
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
    } //����� ������ � ������� ����
    public void ExitGameButton()
    {
        Application.Quit();
    } //����� �� ���� ����� ������ � ���� (������ ����� ����)
}