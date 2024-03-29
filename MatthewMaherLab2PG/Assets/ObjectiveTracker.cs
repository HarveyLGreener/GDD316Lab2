using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectiveTracker : MonoBehaviour
{
    public int wood= 0;
    public int rock = 0;
    public int woodNeeded = 10;
    public int rocksNeeded = 10;
    public TextMeshProUGUI woodText; 
    public TextMeshProUGUI rockText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        woodText.text = "Wood Gathered: " + wood + "/" + woodNeeded;
        rockText.text = "Rocks Gathered: " + rock + "/" + rocksNeeded;
        if (wood >= woodNeeded && rock >= rocksNeeded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
