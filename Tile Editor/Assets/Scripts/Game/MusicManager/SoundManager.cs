using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioClip collectPointClip;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayCollectPoint()
	{
		audio.PlayOneShot(collectPointClip,NGUITools.soundVolume);
	}
}
