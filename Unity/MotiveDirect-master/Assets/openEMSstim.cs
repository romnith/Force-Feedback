using System.Collections;
using System.IO.Ports;
using UnityEngine;

/**
 * Sends messages to the RehaStim Device
 */

public class openEMSstim : MonoBehaviour
{
	private static SerialPort Port;
	public string PortName = "/dev/tty.wchusbserial1410";

    private static string incomingMsg;
    public bool test;
    public string dataOut, dataLastOut;

    // Use this for initialization
    public void Start()
	{
		init();
		print("Init done on Serial Port: " + PortName);
	}

    //public void Update()
    //{
    //    //if (openEMSstim.Port.IsOpen)
    //    //{
    //        try
    //        {
    //            incomingMsg = openEMSstim.Port.ReadLine();
    //            // Ignore heartbeat message
    //            if (incomingMsg != "0")
    //            {
    //                //Debug.Log("EMS: " + incomingMsg);
    //            }

    //            // Avoid sending same value too many times
    //            //if (dataOut != dataLastOut)
    //            //{
    //            //    Debug.Log("EMS: " + dataOut + " VS " + dataLastOut);
    //            //    dataLastOut = dataOut;
    //            //    sendMessage(System.Text.Encoding.UTF8.GetBytes(dataLastOut));
    //            //}
    //        }
    //        catch (System.Exception e)
    //        {
    //            // Heartbeat message, does nothing
    //            //sendMessage(System.Text.Encoding.UTF8.GetBytes(dataLastOut));
    //            Debug.Log("EMS: " + e);
    //        }
    //    //}
    //}

    private void OnDestroy()
	{
		close();
	}

	protected void init()
	{
		openEMSstim.Port = new SerialPort();
		openEMSstim.Port.PortName = PortName;
		openEMSstim.Port.BaudRate = 19200;
		//openEMSstim.Port.Parity = Parity.None;
		//openEMSstim.Port.DataBits = 8;
		//openEMSstim.Port.StopBits = StopBits.Two;
		openEMSstim.Port.Open();
		//print("Opening Serial Port: " + PortName);
	}

	protected void close()
	{
		if (openEMSstim.Port != null)
		{
			openEMSstim.Port.Close();
			openEMSstim.Port.Dispose();
			//print("Closed Serial Port " + RehaStimInterface.Port.PortName);
			openEMSstim.Port = null;
		}
	}

	public bool sendMessage(byte[] message)
	{
		SerialPort sp = openEMSstim.Port;
		if (sp == null || !sp.IsOpen)
		{
			//print ("Error: Serial Port not open");
			return false;
		}
		sp.Write(message, 0, message.Length);
		//print(System.BitConverter.ToString(message));
        //Debug.Log("EMS message out: " + message.ToString());
		return true;
	}

    public string readMessage()
    {
        string msg = "EMS message out: " + openEMSstim.Port.ReadLine();
        return msg;
    }
}