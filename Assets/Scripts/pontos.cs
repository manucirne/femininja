using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pontos : MonoBehaviour
{
    public Text fem;
    public Text poin;
    int feminism = 0;
    int ninjaPoint = 4;
    // Start is called before the first frame update
    void Start()
    {
        feminism = GameObject.Find("Ninja").GetComponent<PlayerController>().feminism;
        ninjaPoint = GameObject.Find("Ninja").GetComponent<PlayerController>().ninjaPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        feminism = GameObject.Find("Ninja").GetComponent<PlayerController>().feminism;
        ninjaPoint = GameObject.Find("Ninja").GetComponent<PlayerController>().ninjaPoint;
        fem.text = "FemiPoints: " + feminism;
        poin.text = "Vidas: " + ninjaPoint;
        
    }
}
