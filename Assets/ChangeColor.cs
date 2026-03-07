using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{   
    public GameObject model;
    public Color color;
    public Material colorMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor_BTN()
    {
    
    Renderer rend = model.GetComponentInChildren<Renderer>();

        if (rend != null)
        {
            rend.material.color = color;
            Debug.Log("Color cambiado exitosamente.");
        }
        else
        {
            Debug.LogError("No se encontró ningún componente Renderer en el modelo o sus hijos.");
        }
    }
}
