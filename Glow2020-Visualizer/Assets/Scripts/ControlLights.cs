using AuraAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(fillColors());
        }
    }

    private Color GetRandomColour()
    {
        return new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.5f,1.0f));
    }

    List<Color> rememberLights = new List<Color>();
    
    private IEnumerator fillColors()
    {
        lights.Clear();
        foreach(GameObject gameObject in rings)
        {
            lights.AddRange(gameObject.GetComponentsInChildren<Light>());
        }
        //18 = buitenste
        // 18 - 30 is middel
        // laatste 8 = binnenste
        for(int p = 0; p < lights.Count; p++)
        {
            rememberLights.Add(lights[p].color);
        }

        for(int y = 0; y < lights.Count; y++)
        {
            if(y < 18){
                lights[y].color = new Color(1,0,0,1);
            } else if(y < 30){
                lights[y].color = new Color(0,1,0,1);
            } else {
                lights[y].color = new Color(0,0,1,1);
            }
            // lights[y].color = GetRandomColour();
            yield return new WaitForSeconds( 0.1f );
        }

        yield return new WaitForSeconds( 4.1f );

        for(int q = 0; q < rememberLights.Count; q++)
        {
            lights[q].color = rememberLights[q];
        }
    } 
}
