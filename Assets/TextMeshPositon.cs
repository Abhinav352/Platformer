using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextMeshPositon : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Vector3 desiredPosition;

    private void Start()
    {
        // Set the desired position
        textMeshPro.transform.position = desiredPosition;
    }
}