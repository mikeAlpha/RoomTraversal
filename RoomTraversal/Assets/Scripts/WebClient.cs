using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System;

public class WebClient : MonoBehaviour
{
    public static WebClient instance;
    WebSocket ws;

    public string ReceivedInfo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            ReceivedInfo = System.Text.Encoding.UTF8.GetString(e.RawData);
            Debug.Log("Received From " + ((WebSocket)sender).Url + ", Data : " + ReceivedInfo);
        };
    }

    public void Send(string msg)
    {
        ws.Send(msg);
    }
}
