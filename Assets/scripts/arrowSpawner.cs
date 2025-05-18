using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrowSpawner : MonoBehaviour
{
    [SerializeField] private spawnNotes noteSpawner;
	[SerializeField] private playerMovement player;

	private Transform pointWithPlayer;
	private Transform pointOnNextNote;
	private LineRenderer lr;


	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
		lr.positionCount = 2;
	}

	private void Update()
	{
		pointWithPlayer = noteSpawner.notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetStartPoint().transform;
		pointOnNextNote = noteSpawner.notesToRender[player.GetNextNoteIndex(player.GetCurrentNoteIndex())].GetComponent<noteScript>().GetEndPoint().transform;

		lr.SetPosition(0, pointWithPlayer.position);
		lr.SetPosition(1, pointOnNextNote.position);


	}

}
