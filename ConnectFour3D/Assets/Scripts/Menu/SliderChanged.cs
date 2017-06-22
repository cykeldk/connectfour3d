using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChanged : MonoBehaviour {

    public Slider mainSlider;
    //Invoked when a submit button is clicked.
    public void UpdateDifficultySetting(float val)
    {
        Debug.Log(val);
        PreGameSettings.setDifficulty((int)val);
    }

    // Use this for initialization
    public void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { UpdateDifficultySetting(mainSlider.value); });
    }
}
