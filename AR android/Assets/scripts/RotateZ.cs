using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotateZ : MonoBehaviour
{
    public int basicSpeed;
    private int speed;

    public void SetSpeed(int speedToSet)
    {
        speed = speedToSet;
    }

    public void ResetZRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(-30.0f, 30.0f));
    }
    void Start()
    {
        speed = basicSpeed;
    }
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
        
        if (IsBetween(gameObject.transform.rotation.z, 0.4, 0.9) && speed > 0  )
        {
            speed = -speed;
        }
        if (IsBetween(gameObject.transform.rotation.z, -0.4, -0.9) && speed < 0) 
        {
            speed = -speed;
        }

    }

    public bool IsBetween(double testValue, double bound1, double bound2)
    {
        if (bound1 > bound2)
            return testValue >= bound2 && testValue <= bound1;
        return testValue >= bound1 && testValue <= bound2;
    }
}
