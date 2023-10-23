using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] private float dayRotation = 51.414f;
    [SerializeField] private float nightRotation = -134.183f;
    [SerializeField] private float rotationSpeed= 5.0f;

    private Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        //SetTime(true);   
    }

    public void SetTime(bool dayTime)
    {
        Vector3 eulerRot = new Vector3();
        if (dayTime) eulerRot = new Vector3(dayRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            
        if (!dayTime) eulerRot = new Vector3(nightRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        targetRotation = Quaternion.Euler(eulerRot);
    }

        // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime*rotationSpeed);
    }
}
