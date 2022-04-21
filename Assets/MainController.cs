using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Configuration;
using System.Globalization;
using System.Text;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
    public float enlargeSpeed;
    public float enlargeScale;
    public float enlargeHeight;

   

    private IPAddress ips;
    private int port;
    private byte[] bytes = new byte[1024];

    private Dictionary<int, UdpClient> senders = new Dictionary<int, UdpClient>();
    private Dictionary<int, IPEndPoint> ipEndPoints = new Dictionary<int, IPEndPoint>();

    #region private

    public void SetupSocket()
    {
        var ipString = @"127.0.0.1";
        port = 10998;
        ips = IPAddress.Parse(ipString);
        ipEndPoints.Add(port, new IPEndPoint(ips, port));
        senders.Add(port, new UdpClient());

    }
    #endregion
    public void SendCommand(string command)
    {
        port = 10998;
        byte[] msg = Encoding.UTF8.GetBytes(command);
        int bytesSent = senders[port].Send(msg, msg.Length, ipEndPoints[port]);
        Debug.Log(command);
        Debug.Log(port);
    }
    // Use this for initialization
    void Start()
    {

        SetupSocket();
        Debug.Log(port);
        Debug.Log(ips);
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.TriggerEvent("Reset");
        }
    }
}
