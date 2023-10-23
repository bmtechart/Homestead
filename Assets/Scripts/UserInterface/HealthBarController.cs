using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image HealthBarFill;
    private float currentFill;
    private float targetFill;
    [SerializeField] private float fillChangeSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnHealthChanged(float healthPercentage)
    {
        targetFill = healthPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFill.fillAmount = Mathf.Lerp(HealthBarFill.fillAmount, targetFill, fillChangeSpeed * Time.deltaTime);
    }
}
