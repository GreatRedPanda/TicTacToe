using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector2 ScaleClamp = new Vector2(2.5f, 20);
    public float YSmoothDampSpeed = 10;
    public float ScaleSpeed = 0.002f;
    float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float msd = Input.GetAxis("Mouse ScrollWheel");
         Zoom(msd );
    }

    public float Zoom(float mouseScrollDelta)
    {




        float deltaMagnitudeDiff = mouseScrollDelta;

        scale += deltaMagnitudeDiff * ScaleSpeed;


        scale = Mathf.Clamp(scale, ScaleClamp.x, ScaleClamp.y);
        transform.localScale = Vector3.one * scale;

        return scale;
    }

}
