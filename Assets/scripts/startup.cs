using UnityEngine;

public class startup : MonoBehaviour
{

    [SerializeField] private GameObject[] background;
    void Awake()
    {
        foreach (GameObject go in background)
        {
            go.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
