using System;
using UnityEngine;

public class playNoteSound : MonoBehaviour
{

	[SerializeField] private playerMovement player;
	[SerializeField] private spawnNotes noteSpawner;
	[SerializeField] private playMetronome metronome;
	[SerializeField] private AudioClip[] allQuarterNotes;
	[SerializeField] private AudioClip[] allHalfNotes;
	[SerializeField] private AudioClip[] allWholeNotes;
	private AudioSource audioSource;

	private string pitch;
	private float duration;
	int index = 0;



	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySoundOfCurrentNote()
	{
		pitch = noteSpawner.notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetPitch();
		duration = noteSpawner.notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetNoteDuration(noteSpawner.notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetNoteType());

		if (duration == 1f)
		{
			index = GetIndex(pitch);
			audioSource.PlayOneShot(allQuarterNotes[index]);
		}
		else if (duration == 2f)
		{
			index = GetIndex(pitch);
			audioSource.PlayOneShot(allHalfNotes[index]);
		}
		else if (duration == 4f)
		{
			index = GetIndex(pitch);
			audioSource.PlayOneShot(allWholeNotes[index]);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private int GetIndex(string pitch)
	{
		switch (pitch)
		{
			case "D4":
				return 0;
			case "E4":
				return 1;
			case "F4":
				return 2;
			case "G4":
				return 3;
			case "A4":
				return 4;
			case "B4":
				return 5;
			case "C5":
				return 6;
			case "D5":
				return 7;
			case "E5":
				return 8;
			case "F5":
				return 9;
			case "G5":
				return 10;
			default:
				return 0;
		}

	}
}
