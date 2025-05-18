using NUnit.Framework;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class setUpQuestions : MonoBehaviour
{
    [SerializeField] private GameObject intervalBox1; //for intervals
    [SerializeField] private GameObject intervalBox2;// for intervals
    [SerializeField] private GameObject noteBox; //for note questions
    [SerializeField] private noteList noteList;
    [SerializeField] private spawnNotes noteSpawner;
    [SerializeField] private playerMovement player;
    [SerializeField] private TextMeshProUGUI[] textsBox1 = new TextMeshProUGUI[2];
	[SerializeField] private TextMeshProUGUI[] textsBox2 = new TextMeshProUGUI[2];

	private string[] intervalList = {"same note", "second", "major 3rd", "minor 3rd", "fourth", "fifth", "minor 6th", "major 6th", "minor 7th", "major 7th", "octave"};
    private string[] returnList = new string[2];

    private string[] questionTypes = { "interval", "note"};

	private void Awake()
	{
		intervalBox1.SetActive(false);
        intervalBox2.SetActive(false);
        noteBox.SetActive(false);
	}

	private void Start()
	{
		player.OnPlayerJump += WhenPlayerJumps;
	}

	private void WhenPlayerJumps(object sender, System.EventArgs e)
	{
		//throw new System.NotImplementedException();
	}

	public string[] ResetQuestion()
    {
        string questionType = questionTypes[UnityEngine.Random.Range(0, 1)];

        // if we do an interval question--------------------------------------------------------------------------------------------
        if (questionType == "interval")
        {
            intervalBox1.SetActive(true);
            intervalBox2.SetActive(true);
            noteBox.SetActive(false);

            //yes i know this is goofy it basically gets the interval between two notes :P
            string interval = noteList.GetInterval(noteSpawner.notesToRender[player.GetCurrentNoteIndex()].GetComponent<noteScript>().GetPitch(), noteSpawner.notesToRender[player.GetNextNoteIndex(player.GetCurrentNoteIndex())].GetComponent<noteScript>().GetPitch());
            Debug.Log(interval + "is the interval");
            int correctBox = UnityEngine.Random.Range(1, 3);


			//set a random key to tbe the right answer and link it with the right box, show the boxes
			if (correctBox == 1)
            {
                textsBox1[0].text = $"{interval}?";
				textsBox1[1].text = "1";


				string fakeInterval = intervalList[UnityEngine.Random.Range(0, 8)]; //set second answerbox to the wrong answer

                while(fakeInterval == interval)
                {
                    fakeInterval = intervalList[UnityEngine.Random.Range(0, 8)];

				}

                textsBox2[0].text = $"{fakeInterval}?";
				textsBox2[1].text = "2";
			}
            else if (correctBox == 2)
            {
				textsBox2[0].text = $"{interval}?";
				textsBox2[1].text = "2";

				string fakeInterval = intervalList[UnityEngine.Random.Range(0, 8)]; //set first answerbox to the wrong answer

				textsBox1[0].text = $"{fakeInterval}?";
				textsBox1[1].text = "1";
			}

            returnList[0] = correctBox.ToString();
            returnList[1] = interval;
            return returnList;
        }
        else if (questionType == "note")//note questions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		{
			intervalBox1.SetActive(false);
			intervalBox2.SetActive(false);
			noteBox.SetActive(true); //question is already pretyped in the editor

            //gets the note after the one the player is currently on
            string nextPitch = noteSpawner.notesToRender[player.GetNextNoteIndex(player.GetCurrentNoteIndex())].GetComponent<noteScript>().GetPitch();

            string nextNote = nextPitch.Remove(1, 1);
            Debug.Log(nextNote + " is the next note");

            

            returnList[0] = nextNote;
			returnList[1] = "amongus";
			return returnList;

		}
        else
        {
            returnList[0] = "chop";
            returnList[1] = "chin";

            return returnList;
        }
    }
}
