using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	public AudioClip[] audioClip;
	// Use this for initialization
	void Start ()
	{
		PlayMusic();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void PlayMusic()
	{
		audio.clip= audioClip[0];
		audio.loop = true;
		audio.Play();
	}
	public void SetMusicVolumn(float volumn)
	{
		audio.volume = volumn;
	}
	public float GetMusicVolumn()
	{
		return audio.volume;
	}
}
