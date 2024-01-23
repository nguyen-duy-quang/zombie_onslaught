using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapImage : MonoBehaviour
{
    public Image map;

    public ScriptableObjectMapImage mapImage;

    private void Start()
    {
        DisplayMapImage();
    }

    private void DisplayMapImage()
    {
        map.sprite = mapImage.mapIcon;
    }    
}
