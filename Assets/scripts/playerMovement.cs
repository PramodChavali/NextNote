using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	[SerializeField] private spawnNotes noteSpawner;
	[SerializeField] private setUpQuestions setUpQuestions;
	[SerializeField] private playMetronome metronome;
	[SerializeField] private GameObject beatDisplay;
	[SerializeField] private playNoteSound pianoSpeaker;
	private int currentNoteIndex = 1;
	[SerializeField] private float playerMoveSpeed = 1f;
	private Vector2 newPlayerPos;
	public event EventHandler OnPlayerJump;
	public event EventHandler OnKeyPress;
	private string[] answerArray = new string[2];
	private Vector2 playerPos;
	private float nextJumpTime;
	private float jumpLeeway = 2f;
	private bool safeToJump;
	private float score;

	void Start()
	{
		playerPos = noteSpawner.notesToRender[1].GetComponent<noteScript>().GetPlayerPoint().transform.position;
		transform.position = playerPos;
		OnPlayerJump += WhenPlayerJumps;
		OnKeyPress += WhenKeyIsPressed;
		answerArray = setUpQuestions.ResetQuestion();
		nextJumpTime = GetNextJumpTime();
		pianoSpeaker.PlaySoundOfCurrentNote();
	}

	private void WhenKeyIsPressed(object key, EventArgs e)
	{
		float timeKeyPressed = Time.time;

		// Rest of your key detection code...

		if (answerArray[1] != "amongus") //interval question
		{
			if (answerArray[0] == "1" && (KeyCode)key == KeyCode.Alpha1 ||
				answerArray[0] == "2" && (KeyCode)key == KeyCode.Alpha2)
			{
				// Calculate time difference from perfect jump moment
				float timeDifference = Mathf.Abs(timeKeyPressed - nextJumpTime);

				Debug.Log($"Jump time: {nextJumpTime}, Pressed at: {timeKeyPressed}, Diff: {timeDifference}, Leeway: {jumpLeeway}");

				if (timeDifference <= jumpLeeway)
				{
					safeToJump = true;
					Debug.Log("Successful jump!");
				}
				else
				{
					safeToJump = false;
					Debug.Log($"Missed! Off by {timeDifference - jumpLeeway} seconds");
				}

				if (safeToJump)
				{
					score += 100f;
					int nextNoteIndex = currentNoteIndex + 1;
					//gets the next place to set the playerpos to
					while (true)
					{
						if (noteSpawner.notesToRender[nextNoteIndex].GetComponent<noteScript>().GetNoteType() != "barline")
						{
							newPlayerPos = noteSpawner.notesToRender[nextNoteIndex].GetComponent<noteScript>().GetPlayerPoint().transform.position;

							break;
						}
						else
						{
							nextNoteIndex += 1;
						}

					}

					//transform.position = Vector2.Lerp(playerPos, newNotePos, playerMoveSpeed * Time.deltaTime);
					currentNoteIndex++;
					transform.position = newPlayerPos;
					//position changed! FIRE THE EVENTTT
					OnPlayerJump?.Invoke(this, EventArgs.Empty);
					

				}
				else
				{
					noteSpawner.EndGame(false);
				}
			}
		}
	}

	private void WhenPlayerJumps(object sender, EventArgs e)
	{
		pianoSpeaker.PlaySoundOfCurrentNote();
		answerArray = setUpQuestions.ResetQuestion();

	}

	// Update is called once per frame
	void Update()
	{
		playerPos = noteSpawner.notesToRender[currentNoteIndex].GetComponent<noteScript>().GetPlayerPoint().transform.position;
		transform.position = playerPos;

		beatDisplay.GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<TextMeshProUGUI>().text = ((int)metronome.beatInBar).ToString();

		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(key))
			{
				OnKeyPress?.Invoke(key, EventArgs.Empty);
			}
		}

	}
	public int GetCurrentNoteIndex()
	{
		return currentNoteIndex;
	}

	public int GetNextNoteIndex(int noteIndex)
	{
		int nextNoteIndex = noteIndex + 1;

		while (true)
		{
			if (noteSpawner.notesToRender[nextNoteIndex].GetComponent<noteScript>().GetNoteType() != "barline")
			{
				newPlayerPos = noteSpawner.notesToRender[nextNoteIndex].GetComponent<noteScript>().GetPlayerPoint().transform.position;

				break;
			}
			else
			{
				nextNoteIndex += 1;
			}


		}
		return nextNoteIndex;

	}

	public float GetNextJumpTime()
	{
		float duration = noteSpawner.notesToRender[currentNoteIndex].GetComponent<noteScript>().GetNoteDuration(noteSpawner.notesToRender[currentNoteIndex].GetComponent<noteScript>().GetNoteType());
		//got the duration of next note

		float timeUntilNextJump = metronome.secondsPerBeat * duration;
		float timeOfNextJump = Time.time + timeUntilNextJump;
		return timeOfNextJump;
	}
}