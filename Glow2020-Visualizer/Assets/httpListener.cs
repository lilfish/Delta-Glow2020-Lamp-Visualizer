using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;

using AuraAPI;
using System.Collections;
using System.Collections.Generic;

public class httpListener : MonoBehaviour
{

	private HttpListener listener;
	private Thread listenerThread;
    private CustomLightList parent;

    public List<GameObject> rings;
    List<Light> lights = new List<Light>();

    [Serializable]
    public class CustomLight
    {
        public string id;
        public string value;
    }
    [Serializable]
    public class CustomLightList
    {
        public CustomLight[] lights;
    }

	void Start ()
	{
        foreach(GameObject gameObject in rings)
        {
            lights.AddRange(gameObject.GetComponentsInChildren<Light>());
        }

		listener = new HttpListener ();
		listener.Prefixes.Add ("http://localhost:4444/");
		listener.Prefixes.Add ("http://127.0.0.1:4444/");
		listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
		listener.Start ();

		listenerThread = new Thread (startListener);
		listenerThread.Start ();
		Debug.Log ("Server Started");
	}

	void Update ()
	{
        if(parent != null && parent.lights != null){ 
            foreach(var light in parent.lights)
            {
                string[] split = light.value.Split(","[0]);
                for(int p = 0; p < lights.Count; p++)
                {
                    if(int.Parse(light.id) == p){
                        lights[p].color = new Color(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                        break;
                    }
                }
            }
            parent = new CustomLightList();
        }
	}

	private void startListener ()
	{
		while (true) {               
			var result = listener.BeginGetContext (ListenerCallback, listener);
			result.AsyncWaitHandle.WaitOne ();
		}
	}

	private void ListenerCallback (IAsyncResult result)
	{				
		var context = listener.EndGetContext (result);		
		if (context.Request.QueryString.AllKeys.Length > 0)
			foreach (var key in context.Request.QueryString.AllKeys) {
				Debug.Log ("Key: " + key + ", Value: " + context.Request.QueryString.GetValues (key) [0]);
			}

		if (context.Request.HttpMethod == "POST") {	
			Thread.Sleep (1000);
			var data_text = new StreamReader (context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd ();
            parent = JsonUtility.FromJson<CustomLightList>(data_text);
		}

		context.Response.Close ();
	}

}