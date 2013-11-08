using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class HttpManager : MonoBehaviour {
	
		//url
		private string mServerAddress = "";
		
		//func name
	    private string funGameInit = "v1/gameInit?";
		
		//comon parameter
	    private string os = "1";
	    private string socialPlatform = "12";
	    private string version = "1.0";
	    private string channel = "111";
		// private parameter
		
		
		

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		public List<NameValuePair> InitCommParm()
		{
			List<NameValuePair> nameValuePair = new List<NameValuePair>();
			
			nameValuePair.Add(new NameValuePair("os",os));
			nameValuePair.Add(new NameValuePair("socialPlatform",socialPlatform));
			nameValuePair.Add(new NameValuePair("version",version));
			nameValuePair.Add(new NameValuePair("channel",channel));
			
			
			return nameValuePair;
		}
	
		public IEnumerator Request(string requestName,List<NameValuePair> paramsList, string urlPrefix, HTTPMETHOD methodType, RequestCallback callback)
		{
			WWW www = null;
			string url = string.Empty;
			url = mServerAddress + urlPrefix;
			
			if(methodType == HTTPMETHOD.GET)
			{
				for(int i = paramsList.Count-1; i >=0; i --)
				{
					url = url + paramsList[i].mKey + "=" + paramsList[i].mValue + "&";
				}
				url = url.Remove(url.Length - 1);
				www = new WWW(url);
			}
			else 
			{
				url = url.Remove(url.Length - 1);
				WWWForm wwwForm = new WWWForm();
				for(int i = paramsList.Count-1; i >=0; i --)
				{
					wwwForm.AddField(paramsList[i].mKey,paramsList[i].mValue);
				}
				www= new WWW(url, wwwForm);
			}
				
			Debug.Log(requestName+" ::::::::: "+url);
			yield return www;
			
			
			if (www.error == null)
			{
				callback.OnRequestSuccess(requestName,www.text);
			}
			else
			{
				//网络异常,for example :no net
			
				callback.OnRequestFailed(requestName);
			}
		}
	
		public void GameInit(string uid, RequestCallback callback)
		{
		        List<NameValuePair> paramsList = new List<NameValuePair>();
		        paramsList = InitCommParm();
		        paramsList.Add(new NameValuePair("uid",uid));

				StartCoroutine(Request("GameInit", paramsList, funGameInit, HTTPMETHOD.GET, callback));
		}
	
}

		public class NameValuePair
		{
			public string mKey;
			public string mValue;
			public NameValuePair(string key, string value)
			{
				mKey = key;
				mValue = value;
			}
		}
		public enum HTTPMETHOD
		{
			GET,
			POST
		}

