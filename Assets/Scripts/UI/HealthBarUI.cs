using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public GameObject healthUIPrefab;
    public Transform barPoint;
    public bool alwaysVisible;
    public float visibleTime;
    Image healthSlider;
    Transform UIbar;
    Transform cam;
    CharacterStates currentStates;
    private bool  findCam;
    void Awake()
    {
        currentStates = GetComponent<CharacterStates>();
        currentStates.UpdateHealthBarOnAttack += UpdateHealthBar;
    }
    private void Start()
    {
        cam = Camera.main.transform;
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                UIbar = Instantiate(healthUIPrefab, canvas.transform).transform;
                healthSlider = UIbar.GetChild(0).GetComponent<Image>();
                UIbar.gameObject.SetActive(alwaysVisible);

            }
        }
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0 && UIbar != null)
        {
            UIbar.gameObject.SetActive(false);
            Destroy(UIbar.gameObject,2f);
        }
        else
        {
            UIbar.gameObject.SetActive(true);
            float sliderPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = sliderPercent;
        }


    }
    private void LateUpdate()
    {
        if (UIbar != null)
        {
            UIbar.position = barPoint.position;
            UIbar.forward = -cam.forward;
        }
    }
}
