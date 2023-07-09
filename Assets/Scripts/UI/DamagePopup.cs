using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fadeDuration = 1f;

    private TextMeshPro textMesh;
    private float destroyTime;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        textMesh.alpha -= Time.deltaTime / fadeDuration;

        if (Time.time >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void ShowDamage(int damage)
    {
        textMesh.SetText(damage.ToString());
        destroyTime = Time.time + fadeDuration;
    }
}