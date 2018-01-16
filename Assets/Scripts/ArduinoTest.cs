using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;




public class ArduinoTest : MonoBehaviour
{
    public float speed;
    private float amountToMove;

    SerialPort serialPort = new SerialPort("COM9", 9600);

    private void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 1;
    }

    void Update()
    {
        amountToMove = speed * Time.deltaTime;

        if(serialPort.IsOpen)
        {
            try
            {
                MoveObject(serialPort.ReadByte());
                print(serialPort.ReadByte());
            }
            catch(System.Exception)
            {

            }
        }
    }

    void MoveObject(int Direction)
    {
        if(Direction == 1)
        {
            transform.Translate(Vector3.up * amountToMove, Space.World);
        }
        if(Direction == 2)
        {
            transform.Translate(Vector3.down * amountToMove, Space.World);
        }
    }
}

