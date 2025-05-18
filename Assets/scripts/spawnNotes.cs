using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class spawnNotes : MonoBehaviour
{
	[SerializeField] private noteList pitchToHeight;
	[SerializeField] private playMetronome metronome;
	[SerializeField] private GameObject quarterNote;
	[SerializeField] private GameObject halfNote;
	[SerializeField] private GameObject wholeNote;
	[SerializeField] private GameObject barline;
	[SerializeField] float moveSpeed;
	[SerializeField] private GameObject trebleClef;
	[SerializeField] private playerMovement player;
	[SerializeField] private GameObject winLoseBanner;

	private string[] noteNames = { "D4", "E4", "F4", "G4", "A4", "B4", "C5", "D5", "E5", "F5", "G5" };
	private float[] durationList = {1f, 2f, 4f};
	private float beatsInBar = 0f;
	private float xPos = -7f;
	private GameObject staffObj;
	private Vector2 position;
	private float totalBeats = 0f;
	private List<List<object>> staffNotesList = new List<List<object>>();
	public List<GameObject> notesToRender = new List<GameObject>();
	private GameObject staffObjVisual;
	private bool barlined = false;
	private int index = 0;


	public bool gameRunning = false;
	private bool win = false;




	private void Awake()
	{

		winLoseBanner.gameObject.SetActive(false);
		while (totalBeats < 256f) //pregen the notes and then draw them as the player is playing
		{
			List<object> staffObjInfo = new List<object>();
			if (totalBeats % 4 == 0 && barlined == false)
			{
				beatsInBar = 0f;
				staffObjInfo.Add("barline");
				staffObjInfo.Add(xPos);
				staffObjInfo.Add(barline);
				xPos += 5f;
				barlined = true;

				staffObj = new GameObject();
				GameObject objToRender = new GameObject();

				for (int i = 0; i < staffObjInfo.Count; i++)
				{
					Debug.Log(staffObjInfo[i].ToString());
				}
				objToRender = (GameObject) staffObjInfo[2];


				staffObj = Instantiate(objToRender);
				position = staffObj.transform.position;
				position.x = (float) staffObjInfo[1];
				staffObj.transform.position = position;
				staffObj.GetComponent<noteScript>().HideNoteVisual();
				staffObj.GetComponent<noteScript>().SetNoteType("barline");
				notesToRender.Add(staffObj);

			}
			else
			{
				string pitch = noteNames[UnityEngine.Random.Range(0, 11)];
				float noteHeight = pitchToHeight.GetHeightFromPitch(pitch);
				float noteDuration = durationList[UnityEngine.Random.Range(0, 3)];
				
				while ((noteDuration + beatsInBar) > 4)
				{
					noteDuration = durationList[UnityEngine.Random.Range(0, 3)];
				}


				staffObjInfo.Add("note");
				staffObjInfo.Add(xPos);
				staffObjInfo.Add(noteHeight);
				staffObjInfo.Add(pitch);

				switch (noteDuration)
				{
					case 1f:
						staffObjInfo.Add(quarterNote);
						break;
					case 2f:
						staffObjInfo.Add(halfNote);
						break;
					case 4f:
						staffObjInfo.Add(wholeNote);
						break;
				}

				GameObject staffObj;
				GameObject objToRender;

				objToRender = (GameObject) staffObjInfo[4];


				staffObj = Instantiate(objToRender);
				position = staffObj.transform.position;
				position.x = (float) staffObjInfo[1];
				position.y = (float) staffObjInfo[2];
				staffObj.transform.position = position;
				staffObj.GetComponent<noteScript>().HideNoteVisual();
				staffObj.GetComponent<noteScript>().SetNoteType(noteDuration);
				staffObj.GetComponent<noteScript>().SetIndex(index);
				staffObj.GetComponent<noteScript>().SetPitch(pitch);
				

				notesToRender.Add(staffObj);

				totalBeats += noteDuration;
				beatsInBar += noteDuration;
				xPos += 5;
				barlined = false;
				index++;
			}
		}
		
	}

	private void Update()
	{

		RunGame(gameRunning);
	}

	public void RunGame(bool gameRunning)
	{
		if (gameRunning == true)
		{
			Debug.Log("game is running!");
			for (int i = 0; i < notesToRender.Count; i++)
			{
				moveSpeed = notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetMoveSpeed(metronome.secondsPerBeat, notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetNoteType());

				if (notesToRender[i].transform.position.x >= -7f && notesToRender[i].transform.position.x <= 15f)//note is on the screen
				{
					notesToRender[i].GetComponent<noteScript>().ShowNoteVisual();
				}
				if (notesToRender[i].transform.position.x >= -7f)//if note is on screen or off screen to the right
				{

					notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().SetSpeedCalculated(true);
					Vector2 newPos = notesToRender[i].transform.position;
					newPos.x -= moveSpeed * Time.deltaTime;

					if (!float.IsNaN(newPos.x) && !float.IsInfinity(newPos.x))
					{
						Vector3 safePosition = notesToRender[i].transform.position;
						safePosition.x = newPos.x;
						notesToRender[i].transform.position = safePosition;
					}
					else
					{
						Debug.LogWarning($"Invalid position x={newPos.x} for note at index {i}");
					}

					notesToRender[i].transform.position = newPos;
					


				}
				else
				{
					if (notesToRender[i] == notesToRender[player.GetCurrentNoteIndex()])
					{
						gameRunning = false;
					}
					notesToRender[i].GetComponent<noteScript>().HideNoteVisual();
				}

			}
		}
		else
		{
			if (win == false)
			{
				winLoseBanner.GetComponentInChildren<TextMeshProUGUI>().text = "you lose :(";
				winLoseBanner.gameObject.SetActive(true);
			}
			else
			{
				winLoseBanner.GetComponentInChildren<TextMeshProUGUI>().text = "you win :)";
				winLoseBanner.gameObject.SetActive(true);
			}
		}
	}

	public void EndGame(bool win)
	{
		this.win = win;
		gameRunning = false;
	}
}

	


