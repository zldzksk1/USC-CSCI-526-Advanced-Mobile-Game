using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public float maxEnergy = 100.0f;  
    public static float energy = 100.0f;    
    public float depletionRate = 10.0f; 

    public Slider slider;  

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = maxEnergy;
        slider.value = energy;
    }

    private void Update()
    {
        energy -= depletionRate * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0.0f, maxEnergy);
        slider.value = energy;
    }
}