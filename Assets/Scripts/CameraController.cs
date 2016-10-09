using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Canvas gameBoyCanvas;
    public Canvas ZoomedCanvas;
    
    void Start()
    {
        ZoomedCanvas.enabled = false;
        gameBoyCanvas.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ZoomedCanvas.enabled = !ZoomedCanvas.enabled;
            gameBoyCanvas.enabled = !ZoomedCanvas.enabled;
        }
    }
}
