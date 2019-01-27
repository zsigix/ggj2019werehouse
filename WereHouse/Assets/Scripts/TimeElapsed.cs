using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeElapsed : MonoBehaviour
{
    private float totalTime = 60;
    private Text textInstance;
    // Start is called before the first frame update
    void Start()
    {
        textInstance = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        textInstance.text = Mathf.FloorToInt(totalTime).ToString();
        if (totalTime <= 0)
        {
            SceneManager.LoadScene("EndGameScene");
        }
    }
}
