using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class playMetronome : MonoBehaviour
{
	private AudioSource metrenomeClick;
	[SerializeField] private float BPM;
	[SerializeField] private spawnNotes noteSpawner;
	[SerializeField] private GameObject countdown;
	[SerializeField] private GameObject[] textElements;

	public float secondsPerBeat;
	private float timer;
	private int introClicks = 0;
	public float beatInBar = 1f;
	private float eighthNoteSpeed;

	private void Start()
	{
		metrenomeClick = GetComponent<AudioSource>(); //counting eighth notes
		secondsPerBeat = 60f / BPM;
		eighthNoteSpeed = 60f / BPM / 2f;

		timer = eighthNoteSpeed;



	}
	private void Update()
	{


		timer -= Time.deltaTime;
		if (timer <= 0f)
		{

			metrenomeClick.Play();

			beatInBar += 1f;
			timer = secondsPerBeat;
			if (beatInBar == 6f)
			{
				beatInBar = 1f;
				noteSpawner.gameRunning = true;
			}
		}

	}
}

