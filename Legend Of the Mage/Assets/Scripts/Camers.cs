using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Camers : MonoBehaviour
{
    public Camera camera;
    public Image image;

    void Update()
    {
        //Update the camera's field of view to be the variable returning from the Slider
        if(camera.fieldOfView <= 5)
        {
            SceneManager.LoadScene(1);
            image.color = new Color(0, 0, 0);
        }
        else
        {
            camera.fieldOfView -= 0.08f;
        }
    }
}
