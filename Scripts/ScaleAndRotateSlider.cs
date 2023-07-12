using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScaleAndRotateSlider : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider rotationSlider;

    private float angleSliderNumber;
    private float scaleSliderNumber;


       // Update is called once per frame
    void Update()
    {
        scaleSliderNumber = scaleSlider.value;
        Vector3 scale = new Vector3(scaleSliderNumber, scaleSliderNumber, scaleSliderNumber);
        this.transform.localScale = scale;
        try
        {
            angleSliderNumber = rotationSlider.value * 10f;
            this.transform.rotation = Quaternion.Euler(0, angleSliderNumber, 0);

            
        }
        catch 
        { }
    }
}
