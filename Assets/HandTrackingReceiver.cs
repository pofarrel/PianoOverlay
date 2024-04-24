using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class HandTrackingReceiver : MonoBehaviour
{
    public GameObject handMarker; 
    public int port = 8051;  
    private UdpClient udpClient;
    private Thread udpThread;

    void Start()
    {
        udpClient = new UdpClient(port);
        udpThread = new Thread(new ThreadStart(ThreadMethod));
        udpThread.IsBackground = true;
        udpThread.Start();
    }

    private void ThreadMethod()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref anyIP);

                if (data.Length == sizeof(float) * 2)
                {
                    float x = BitConverter.ToSingle(data, 0);
                    float y = BitConverter.ToSingle(data, sizeof(float));
                    
                    // Since Unity runs not in the thread this UDP thread is on, we use Unity's main thread to update the position
                    UnityMainThreadDispatcher.Instance().Enqueue(() => UpdateHandMarkerPosition(x, y));
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
    }

    private void UpdateHandMarkerPosition(float x, float y)
    {
        float invertedX = 1 - x;
        float invertedY = 1 - y;
        
        Vector3 screenPosition = new Vector3(invertedX * Screen.width, invertedY * Screen.height, 10);  
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        handMarker.transform.position = worldPosition;
    }

    void OnApplicationQuit()
    {
        udpThread.Abort();
        udpClient.Close();
    }
}
