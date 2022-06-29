using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	private static GameController _singleton;
	public static GameController singleton
	{
		get {
			return GameController._singleton;
		}
	}
	public GameObject player;
	public Camera mainCamera;
	public AudioMixer mixer;
	public AudioMixerGroup ambientSoundGroup, backgroundMusicGroup, sfxGroup, backgroundGroup;
	public AudioClip backgroundMusicAudio;
	public AudioSource backgroundMusicSource;
	public AudioClip defaultPickupSFX, defaultBarrierSuccessSFX, defaultBarrierFailSFX, defaultBarrierUnlockSFX;
	public bool isPaused = false;
	public GameObject pauseUI, webPauseUI, finishUI;
	private bool isWeb = (Application.platform == RuntimePlatform.WebGLPlayer);
	// Use this for initialization
	void Awake () {
		GameController._singleton = this;
		if (this.player == null)
			this.player = GameObject.FindGameObjectWithTag ("Player");
		if (this.mainCamera == null)
			this.mainCamera = Camera.main;
		if (this.backgroundMusicSource == null)
			this.mainCamera.gameObject.GetComponent<AudioSource> ();

		this.backgroundMusicSource.clip = this.backgroundMusicAudio;
		this.backgroundMusicSource.loop = true;
		this.backgroundMusicSource.Play ();

		this.pauseUI.SetActive (false);
		this.webPauseUI.SetActive (false);
		this.finishUI.SetActive (false);
	}


	void Update() {
		if(Input.GetButtonDown("Cancel"))
			{
				this.isPaused = !this.isPaused;
			if (this.isWeb)
				this.webPauseUI.SetActive (this.isPaused);
			else
				this.pauseUI.SetActive (this.isPaused);
			
			if (this.isPaused) {
				this.player.SendMessage ("GamePaused");
			} else {
				this.player.SendMessage ("GameResumed");
			}
			}
			
	}
	public void RestartLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void ExitGame() {
		Application.Quit ();
		
	}
	public void PlayerFinished() {
		this.finishUI.SetActive (true);
	}
}
