using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class noteList : MonoBehaviour
{
	private int numHalfSteps;//                     0     1             2      3         4        5        6          7      8          9       10     11         12      13        14     15       16       17
	private string[] allNotesInHalfSteps = {"D4", "D#4", "E4", "F4", "F#4", "G4", "G#4", "A4", "A#4", "B4", "C5", "C#5", "D5", "D#5", "E5", "F5", "F#5", "G5"};


	public float GetHeightFromPitch(string pitch)
	{
		switch (pitch)
		{
			case "D4":
				return -5f;
			case "E4":
				return -4f;
			case "F4":
				return -3f;
			case "G4":
				return -2f;
			case "A4":
				return -1f;
			case "B4":
				return 0f;
			case "C5":
				return 1f;
			case "D5":
				return 2f;
			case "E5":
				return 3f;
			case "F5":
				return 4f;
			case "G5":
				return 5f;
			default:
				return -100f;
		}
	}
	public string GetInterval(string pitch1, string pitch2)
	{
		int indexPitch1 = Array.IndexOf(allNotesInHalfSteps, pitch1);
		int indexPitch2 = Array.IndexOf(allNotesInHalfSteps, pitch2);

		Debug.Log($"pitch1 = {pitch1}, index = {indexPitch1}");
		Debug.Log($"pitch2 = {pitch2}, index = {indexPitch2}");

		if (indexPitch1 < (indexPitch2)) {
			numHalfSteps = indexPitch2 - indexPitch1;
		}
		else if (indexPitch1 > (indexPitch2))
		{
			numHalfSteps = indexPitch1 - indexPitch2;
		}
		else if (indexPitch2 == (indexPitch1))
		{
			return "same note";
		}
		Debug.Log(numHalfSteps.ToString());

		switch (numHalfSteps)
		{
			case 1:
				return "minor 2nd";
			case 2:
				return "major 2nd";
			case 3:
				return "minor 3rd";
			case 4:
				return "major 3rd";
			case 5:
				return "fourth";
			case 6:
				return "tritone";
			case 7:
				return "fifth";
			case 8:
				return "minor 6th";
			case 9:
				return "major 6th";
			case 10:
				return "minor 7th";
			case 11:
				return "major 7th";
			case 12:
				return "octave";
			case 13:
				return "minor 9th";
			case 14:
				return "major 9th";
			case 15:
				return "minor 10th";
			case 16:
				return "major 10th";
			case 17:
				return "aug 11th";
			default:
				return "idfk";

		}
	}
}
