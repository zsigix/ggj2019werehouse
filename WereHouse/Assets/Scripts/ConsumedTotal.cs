using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumedTotal : MonoBehaviour
{
    private Text textInstance;
    // Start is called before the first frame update
    void Start()
    {
        textInstance = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textInstance.text = GlobalFunctions.VictimCount.ToString();
    }
}
