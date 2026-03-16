using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject model;
    public Color color;
    public Material colorMaterial;
    public GameObject[] accesorios; 

    // Mantiene la funcionalidad de cambiar color del modelo base
    public void ChangeColor_BTN()
    {
        Renderer rend = model.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            rend.material.color = color;
            colorMaterial.color = color;
        }
    }

    // Función para accesorios aleatorios (la que ya tienes)
    public void AddRandomAccessory_BTN()
    {
        foreach (GameObject acc in accesorios)
        {
            if (acc != null) acc.SetActive(false);
        }

        if (accesorios.Length > 0)
        {
            int indexAleatorio = UnityEngine.Random.Range(0, accesorios.Length);
            accesorios[indexAleatorio].SetActive(true);
        }
    }

    // --- NUEVA FUNCIÓN: CAMBIA EL COLOR DEL ACCESORIO ACTIVO ---
    public void ChangeAccessoryColor_BTN()
{
    foreach (GameObject acc in accesorios)
    {
        // Solo actuamos sobre el accesorio que esté visible
        if (acc != null && acc.activeSelf)
        {
            // CAMBIO AQUÍ: Obtenemos TODOS los renderers del accesorio y sus hijos
            Renderer[] rends = acc.GetComponentsInChildren<Renderer>();
            
            // Creamos un color aleatorio (RGBA)
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            
            // Aplicamos el color a cada pieza encontrada
            foreach (Renderer r in rends)
            {
                r.material.color = randomColor;
            }
            Debug.Log("Todo el accesorio cambió a: " + randomColor);
        }
    }
}
}