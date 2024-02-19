//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FirebaseNotification : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
//        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
//    }

//    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
//    {
//        Debug.Log("Received Registration Token: " + token.Token);
//    }

//    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
//    {
//        Debug.Log("Received a new message from: " + e.Message.From);
//    }
//}
