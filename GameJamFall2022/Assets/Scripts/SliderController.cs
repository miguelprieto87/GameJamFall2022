using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody playerRB;

    [SerializeField] private Slider slider;

    [Header("Slider Properties")]
    [SerializeField] private float sliderIncreaseAmount;
    [SerializeField] private float sliderDropRate;
    [SerializeField] private float sliderValue;

    private void Update()
    {
        if (playerRB.velocity == Vector3.zero) StartCoroutine("DecreaseSlider");

        if (playerController.isFacingRight == false)
        {
            Vector3 localScale = slider.transform.localScale;
            localScale.x = -1f;
            slider.transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = slider.transform.localScale;
            localScale.x = 1f;
            slider.transform.localScale = localScale;
        }
    }

    public void IncreaseSlider()
    {
        if (sliderValue >= slider.maxValue) return;
        sliderValue += sliderIncreaseAmount;
        slider.value = sliderValue;
        playerController.UpdateStats(sliderValue);
    }

    private IEnumerator DecreaseSlider()
    {
        if (slider.value == 1 || playerRB.velocity != Vector3.zero)
        {
            yield break;
        }

        slider.value = sliderValue;
        sliderValue -= Time.deltaTime * sliderDropRate / 2;
        yield return null;
        playerController.UpdateStats(sliderValue);
        StartCoroutine(DecreaseSlider());
    }
}
