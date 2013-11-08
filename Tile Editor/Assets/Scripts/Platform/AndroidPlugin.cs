using UnityEngine;
using System.Collections;

public class AndroidPlugin : MonoBehaviour {
	
	#if UNITY_ANDROID
	private AndroidJavaClass jc;	
	
	public AndroidJavaObject jo;
   #endif
	
	public void Start()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		#endif
	}
	
    /// <summary>
    /// Shares the init.
    /// </summary>
	public void ShareInit()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		    jo.Call("shareSinaInit");
       #endif
	}
	
	/// <summary>
	/// Shares the message.
	/// </summary>
	/// <param name='type'>
	/// Type. 0 Sina; 1  Tengxun;  2 Renren;  3 Facebook; 
	/// </param>
	/// <param name='content'>
	/// share content  
	/// </param>
	/// <param name='imgData'>
	/// Image data.
	/// </param>
	public void ShareSNS(string content, byte[] imgData,int snsPlatform) // 0 renren 1 sina 2 tenc
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		    jo.Call("ShareSNS", content, imgData, snsPlatform);
       #endif
	}
	
	public void  GetFirendsCallBack(string arg0)
	{
		Debug.Log("frends:" + arg0);
	    Game.dataManager.GetFriendsCallBack(arg0);
		
	}
	

}
