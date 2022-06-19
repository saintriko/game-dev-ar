using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionBall : MonoBehaviour
{
    [FormerlySerializedAs("RotateTest")] public RotateZ RotateObject ;
    public GameObject mainSphere;
    public new UIText textToShow;
    
    private Vector3 startLocalPosition;
    private int collisionsCount = 0;
    void Start()
    {
        startLocalPosition = mainSphere.transform.localPosition;
        Debug.Log(startLocalPosition);
    }

    void Update()
    {
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Sphere")
        {
            StartCoroutine(Touched());
        }
    }
    
    private IEnumerator Touched()
    {
        collisionsCount++;
        textToShow.SetCount(collisionsCount);
        RotateObject.SetSpeed(0);
        mainSphere.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        
        //DELAY sec
        yield return new WaitForSeconds(0.3f);
        RotateObject.ResetZRotation();
        if (mainSphere.transform.localPosition.y >= -0.1)
        {
            mainSphere.transform.localPosition = startLocalPosition;
        }
        else
        {
            mainSphere.transform.localPosition = new Vector3(0, mainSphere.transform.localPosition.y + 0.1f, 0);
        }
        mainSphere.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.1f);
        RotateObject.SetSpeed(RotateObject.basicSpeed);
        yield return new WaitForSeconds(0.1f);
    }
}
