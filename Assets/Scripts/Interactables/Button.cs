using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    
    Renderer objectRenderer;
    [SerializeField] private Color customColor;
    [SerializeField] private Color randomColor1 = Color.white;
    [SerializeField] private Color randomColor2 = Color.white;
    [SerializeField] private Color lerpedColor;
    
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float startTime;

    [SerializeField] private bool lerpOn = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        startTime = Time.time;
    }
    
    void Update()
    {
        
        float t = (Mathf.Sin(Time.time - startTime)) * speed;
        lerpedColor = Color.Lerp(randomColor1, randomColor2, t);

        if(lerpOn)
        {
            objectRenderer.material.SetColor("_Color", lerpedColor);
        }
    }
    protected override void Interact()
    {
        Debug.Log("Porta Aberta!");
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        
        randomColor1 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        randomColor2 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        lerpOn = true;

        //objectRenderer.material.SetColor("_Color", customColor);

        

    }
}
