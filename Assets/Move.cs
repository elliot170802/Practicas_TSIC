using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Move : MonoBehaviour
{   
    [Header("Configuración de Objetos")]
    public GameObject model;
    public ObserverBehaviour[] ImageTargets;
    
    [Header("Ajustes de Movimiento")]
    public float speed = 2.0f;
    public float umbralDistancia = 0.2f;

    [Header("Animación de Rebote")]
    public float bounceHeight = 0.2f; // Qué tan alto salta
    public float bounceSpeed = 12f;   // Qué tan rápido se mueven las piernas/cuerpo

    private bool isMoving = false;

    public void moveToNextMarker() 
    {
        if (!isMoving) StartCoroutine(MoveModel());    
    }

    private IEnumerator MoveModel()
    {
        ObserverBehaviour target = GetNextDetectedTarget();
        if (target == null) yield break;

        isMoving = true;
        Vector3 startPosition = model.transform.position;
        float journey = 0;

        while (journey <= 1.0f)
        {
            // 1. Calculamos el progreso del tiempo
            journey += Time.deltaTime * speed;
            
            // 2. Posición en el suelo (X y Z)
            Vector3 endPosition = target.transform.position;
            Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, journey);
            
            // 3. MATEMÁTICA DEL REBOTE (Eje Y)
            // Mathf.Sin crea una onda que sube y baja. 
            // Mathf.Abs convierte los valores negativos en positivos para que siempre salte hacia arriba.
            float yOffset = Mathf.Abs(Mathf.Sin(journey * bounceSpeed)) * bounceHeight;

            // 4. Aplicamos la posición combinada
            model.transform.position = new Vector3(currentPos.x, currentPos.y + yOffset, currentPos.z);
            
            // 5. Rotación (Mirar al cubo)
            Vector3 direction = new Vector3(endPosition.x, model.transform.position.y, endPosition.z);
            model.transform.LookAt(direction);

            yield return null; 
        }

        // Asegurar que llegue al centro exacto al final
        model.transform.position = target.transform.position;
        isMoving = false;
        Debug.Log("Llegué dando saltos a: " + target.TargetName);
    }

    private ObserverBehaviour GetNextDetectedTarget()
    {
        foreach (ObserverBehaviour target in ImageTargets)
        {
            bool estaVisto = target != null && (target.TargetStatus.Status == Status.TRACKED || target.TargetStatus.Status == Status.EXTENDED_TRACKED);
            if (estaVisto)
            {
                float distancia = Vector3.Distance(model.transform.position, target.transform.position);
                if (distancia > umbralDistancia) return target;
            }
        }
        return null;
    }
}