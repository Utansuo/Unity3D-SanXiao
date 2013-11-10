using UnityEngine;
using System.Collections;

public class SetPanel : BaseUI {
	
	public UISlider musicSlider;
	public UISlider soundSlider;
	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void Close()
	{
		
		base.Close();
	}
	void MusicSliderChange(float volumn)
	{
		Game.musicManager.SetMusicVolumn(volumn);
	}
	void SoundSliderChange(float volumn)
	{
		
		NGUITools.soundVolume = volumn;
		
		
	}
	void OnEnable()
	{
		soundSlider.sliderValue = NGUITools.soundVolume;
		musicSlider.sliderValue = Game.musicManager.GetMusicVolumn();
	}
}
