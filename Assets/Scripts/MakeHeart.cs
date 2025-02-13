// UMD IMDM290 
// Instructor: Myungin Lee
// This tutorial introduce a way to draw spheres and align them in a circle with colors.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHeart : MonoBehaviour
{
    GameObject[] spheres;
    static int numSphere = 100; 
    float time = 0f;
    Vector3[] initPos;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(0, -4, -12.5f);

        spheres = new GameObject[numSphere];
        initPos = new Vector3[numSphere];

        // Let there be spheres..
        for (int i =0; i < numSphere; i++){
            float t = i * 2 * Mathf.PI / numSphere;

            //heart: x = sqrt(2) * sin^3 (t)
            //       y = -cos^3(t) - cos^2(t) + 2cos(t)

            float x = Mathf.Sqrt(2) * Mathf.Pow(Mathf.Sin(t), 3);
            float y = (-1 * Mathf.Pow(Mathf.Cos(t), 3)) - Mathf.Pow(Mathf.Cos(t), 2) + 2 * Mathf.Cos(t);
            float z = 1f;

            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            spheres[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            initPos[i] = new Vector3(6 * x, 6 * y, 6 * z);
            spheres[i].transform.position = initPos[i];

        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float lerpFraction = (Mathf.Sin(time) + 1f) / 2f;

        Vector3 converge = new Vector3(0f, 0f, 10f);
        float start = 0f;
        float end = 360f;

        float currentRotation = Mathf.Lerp(start, end, lerpFraction);

        for (int i = 0; i < numSphere; i++)
        {
            Vector3 newPos = Vector3.Lerp(initPos[i], converge, lerpFraction);

            spheres[i].transform.position = Vector3.Lerp(initPos[i], converge, lerpFraction);
            spheres[i].transform.position = Quaternion.Euler(0f, currentRotation, 0f) * (newPos - converge) + converge;


            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = Mathf.Lerp(0.95f, 0.6f, (float)i / numSphere);
            float saturation = 0.6f;  
            float brightness = 1f;  
            if (Random.value > 0.95f)
                brightness = 5f;
            Color color = Color.HSVToRGB(hue, saturation, brightness);
            sphereRenderer.material.color = color;
        }
    }
}
