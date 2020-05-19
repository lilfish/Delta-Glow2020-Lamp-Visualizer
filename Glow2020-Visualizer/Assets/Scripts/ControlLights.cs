using AuraAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLights : MonoBehaviour
{
    public List<GameObject> rings;
    public bool defaultLightSwitch; 
    public bool defaultVolumetricSwitch;
    public List<bool> lightSwitches;
    public List<bool> volumetricSwitches;
 
    List<Light> lights = new List<Light>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gameObject in rings)
        {
            lights.AddRange(gameObject.GetComponentsInChildren<Light>());
        }

        foreach(Light light in lights)
        {
            bool lightSwitch = new bool();
            lightSwitch = defaultLightSwitch;
            bool volumetricSwitch = new bool();
            volumetricSwitch = defaultVolumetricSwitch;

            lightSwitches.Add(lightSwitch);
            volumetricSwitches.Add(volumetricSwitch);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < lightSwitches.Count; i++)
        {
            lights[i].GetComponent<AuraLight>().enabled = volumetricSwitches[i];
            lights[i].enabled = lightSwitches[i];
            lights[i].GetComponent<AuraLight>().enabled &= lightSwitches[i];
        }
    }
}
