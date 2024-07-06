using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainMenuLight : MonoBehaviour
{
    private float _flickerTimer = 0f;
    private float _flickerChance;
    private bool _canFlicker = false;

    [SerializeField] private Light2D _lampLight;
    [SerializeField] private Cooldown _flickerCooldown;

    private void Start()
    {
        _lampLight = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        LampLightAnimation();
    }

    public void LampLightAnimation()
    {
        if (_lampLight == null)
            return;

        if (_flickerCooldown == null)
            return;

        //Prevent light from flickering too frequently
        if (_flickerCooldown.CurrentProgress == Cooldown.CooldownProgress.Ready)
        {
            _flickerCooldown.Duration = 2f;
            _flickerCooldown.StartCooldown();
        }

        //Start randomizing when light will flicker
        if (_flickerTimer == 0f && _flickerCooldown.CurrentProgress == Cooldown.CooldownProgress.Finished)
        {
            _flickerChance = Random.Range(1, 11);
            Debug.Log($"Chosen number: {_flickerChance}");

            if (_flickerChance < 3f)
            {
                _canFlicker = true;
            }
            else
            {
                _flickerCooldown.StartCooldown();
            }
        }

        //Causes slight flicker to the light
        if (_canFlicker == true)
        {
            _flickerTimer += Time.deltaTime;
            //Debug.Log($"Timer: {_flickerTimer}");

            if (_flickerTimer < 0.2f)
            {
                _lampLight.intensity = Mathf.Lerp(_lampLight.intensity, 0.3f, 0.4f);
                //Debug.Log($"Current light intensity: {_lampLight.intensity}");
            }

            if (_flickerTimer > 0.2f)
            {
                _lampLight.intensity = Mathf.Lerp(_lampLight.intensity, 1f, 0.2f);
                //Debug.Log($"Current light intensity: {_lampLight.intensity}");  
            }

            if (_flickerTimer > 2f)
            {
                _canFlicker = false;
                _flickerTimer = 0f;
                _flickerCooldown.StartCooldown();
            }
        }
    }
}
