using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GoldenSphere : MonoBehaviour
{
    [Range(10,1000)]
    public int numPoints = 100;

    [Range(1, 4)]
    public float radius = 1;
    GameObject CreateSphere(Vector3 vec)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = vec;
        //sphere.tag = "current";
        sphere.transform.localScale = new Vector3(0.2f, .2f, .2f);
        return sphere;
    }

    void ClearObjects()
    {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("current");
        foreach(GameObject e in obs)
        {
            Destroy(e);
        } 
    }

    void GenerateGoldenSphere()
    {
        for(int t = 0; t < numPoints; t++)
        {
            float probability = (t / (float) (numPoints - 1));
            float goldenRatio = (1 + Mathf.Sqrt(5)) * 0.5f;

            // This variable adjusts the rate (cycles / unit rotation) at which points are placed while rotating about the y-axis.
            // Points are dropped at the end of every cycle.
            // The more irrational the overall frequency of rotation is, the more evely spread the points about the y-axis will be.
            float frequencyAdjuster = 2 * (float)Math.PI * goldenRatio;
            float theta = t * frequencyAdjuster;

            // By using inverse transform sampling of the probability distribution function of points around a sphere for arbitrary
            // values of phi, one can provide a cumulative probability and recieve a corresponding phi value. This makes it easy to
            // parameterize phi with respect to theta.
            float phi = (float) Math.Acos(1 - 2 * probability);

            float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
            float y = radius * Mathf.Cos(phi);
            float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);

            CreateSphere(new Vector3(x, y, z));
            Debug.Log($"{x},{y},{z}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGoldenSphere();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
