using System;
using UnityEngine;
using System.IO.Ports;
using TMPro;

enum TaskState
{
    INIT,
    WAIT_COMMANDS
}

public class Unity : MonoBehaviour
{
    private static TaskState taskState = TaskState.INIT;
    private SerialPort _serialPort;
    public TMP_Dropdown ledState;
    public TMP_Dropdown ledNumber;
    public TextMeshProUGUI pulsador1;
    public TextMeshProUGUI pulsador2;
    public TextMeshProUGUI pulsador3;
    
    void Start()
    {
        _serialPort = new SerialPort();
        _serialPort.PortName = "/dev/ttyUSB0";
        _serialPort.BaudRate = 115200;
        _serialPort.DtrEnable = true;
        _serialPort.NewLine = "\n";
        _serialPort.Open();
        Debug.Log("Open Serial Port");
    }

    void Update()
    {
        switch (taskState)
        {
            case TaskState.INIT:
                taskState = TaskState.WAIT_COMMANDS;
                Debug.Log("WAIT COMMANDS");
                break;
            case TaskState.WAIT_COMMANDS:
                
                if (_serialPort.BytesToRead > 0)
                {
                    string response = _serialPort.ReadLine();
                    Debug.Log(response);


                    if (response == "OFFOFFOFF")
                    {
                        pulsador1.text = "RELEASED";
                        pulsador2.text = "RELEASED";
                        pulsador3.text = "RELEASED";
                        
                    }
                    else if (response == "OFFOFFON")
                    {
                        pulsador1.text = "RELEASED";
                        pulsador2.text = "RELEASED";
                        pulsador3.text = "PUSHED";
                    }
                    else if (response == "OFFONOFF")
                    {
                        pulsador1.text = "RELEASED";
                        pulsador2.text = "PUSHED";
                        pulsador3.text = "RELEASED";
                    }
                    else if (response == "OFFONON")
                    {
                         pulsador1.text = "RELEASED";
                         pulsador2.text = "PUSHED";
                         pulsador3.text = "PUSHED";
                    }
                    else if (response == "ONOFFOFF")
                    {
                         pulsador1.text = "PUSHED";
                         pulsador2.text = "RELEASED";
                         pulsador3.text = "RELEASED";
                    }
                    else if (response == "ONONOFF")
                    {
                         pulsador1.text = "PUSHED";
                         pulsador2.text = "PUSHED";
                         pulsador3.text = "RELEASED";
                    }
                    else if (response == "ONONON")
                    {
                         pulsador1.text = "PUSHED";
                         pulsador2.text = "PUSHED";
                         pulsador3.text = "PUSHED";
                    }
                    else if (response == "ONOFFON")
                    {
                        pulsador1.text = "PUSHED";
                        pulsador2.text = "RELEASED";
                        pulsador3.text = "PUSHED";
                    }
                }
            
                break;
            default:
                Debug.Log("State Error");
                break;
        }
    }
    
    public void BTNREADBUTTONS()

    {
        _serialPort.Write("readBUTTONS\n");
        Debug.Log("Send readBUTTONS");

    }
    
    public void LedControl()
    {
        if (ledNumber.options[ledNumber.value].text == "1")
        {
            if (ledState.options[ledState.value].text == "ON")
            {
                _serialPort.Write("Led1ON\n");
                Debug.Log("Send Led1ON");
            }
            else
            {
                _serialPort.Write("Led1OFF\n");
                Debug.Log("Send Led1OFF");
            } 
        }
        else if (ledNumber.options[ledNumber.value].text == "2")
        {
            if (ledState.options[ledState.value].text == "ON")
            {
                _serialPort.Write("Led2ON\n");
                Debug.Log("BSend Led2ON");
            }
            else
            {
                _serialPort.Write("Led2OFF\n");
                Debug.Log("Send Led2OFF");
            } 
        }
        else if (ledNumber.options[ledNumber.value].text == "3")
        {
            if (ledState.options[ledState.value].text == "ON")
            {
                _serialPort.Write("Led3ON\n");
                Debug.Log("Send Led3ON");
            }
            else
            {
                _serialPort.Write("Led3OFF\n");
                Debug.Log("Send Led3OFF");
            } 
        }
    }
}