using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private Slider volumeSlider;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        volumeSlider.onValueChanged.AddListener(gameManager.ChangeVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
