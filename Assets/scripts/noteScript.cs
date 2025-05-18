using System.ComponentModel;
using UnityEngine;

public class noteScript : MonoBehaviour
{
    [SerializeField] private string type;
    [SerializeField] private GameObject visual;
    [SerializeField] private spawnNotes noteSpawner;
    [SerializeField] private playMetronome metronome;

	[SerializeField] private Transform startPoint;
	[SerializeField] private Transform endPoint;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private int indexInList;
    [SerializeField] private string pitch;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool speedCalculated;

 //   public void AssignReference(playMetronome metronome, string objName)
 //   {
 //       metronome = GameObject.Find(objName).GetComponent<playMetronome>();
	//}

    public bool GetSpeedCalculated()
    {
        return speedCalculated;
    }

    public void SetSpeedCalculated(bool speedCalculated)
    {
        this.speedCalculated = speedCalculated;
    }

    public Transform GetStartPoint()
    {
        return startPoint;
    }

    public Transform GetEndPoint()
    {
        return endPoint;
    }

    public Transform GetPlayerPoint()
    {
        return playerPoint;
    }

    public void ShowNoteVisual()
    {
        visual.SetActive(true);
    }
    public void HideNoteVisual()
    {
        visual.SetActive(false);
    }
    public void SetNoteType(string input)
    {
        type = input;
    }
    public string GetNoteType()
    {
        return type;
    }
    public void SetNoteType(float duration)
    {
        if (duration == 1f)
        {
            type = "quarter";
        }
        else if (duration == 2f)
        {
            type = "half";
        }
        else if(duration == 4f)
        {
            type = "whole";
        }
    }

    public void SetIndex(int indexInList)
    {
        this.indexInList = indexInList;
    }

    public int GetIndex()
    {
        return indexInList;
    }

    public string GetPitch()
    {
        return pitch;
    }

    public void SetPitch(string pitch)
    {
        this.pitch = pitch;
    }

    public float GetNoteDuration(string type)
    {
		if (type == "quarter")
		{
            return 1f;
		}
		else if (type == "half")
		{
			return 2f;
		}
		else if (type == "whole")
		{
			return 4f;
		}
        else
        {
            return 0f;
        }
	}

    public float GetMoveSpeed(float secondsPerBeat, string type)
    {
        float distance = transform.position.x - (-8f);
        float totalTime = GetNoteDuration(type) * secondsPerBeat;
        float moveSpeed = distance / totalTime;
        //Debug.Log(moveSpeed.ToString());
        return moveSpeed;
        
    }
}
