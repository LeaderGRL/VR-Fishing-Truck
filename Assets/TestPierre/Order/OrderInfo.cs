using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfo : MonoBehaviour
{

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _orderNumber;
    [SerializeField] private float _timer = 30f;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderImage;
    // Start is called before the first frame update

    public Gradient Gradient => _gradient;
    public int OrderNumber = -1;
    public float TimeRemaining { get; private set; }

    public event Action<OrderInfo> OnTimerFinished;

    private void Start()
    {
        TimeRemaining = _timer;
        Debug.Log("spawn => " + TimeRemaining.ToString());
        _orderNumber.text = OrderNumber.ToString();
        StartCoroutine(UpdateTimer());
    }

    //public void StartTimer()
    //{
    //    StartCoroutine(UpdateTimer());
    //}

    public IEnumerator UpdateTimer()
    {
        while (TimeRemaining > 0)
        {
            //Debug.Log("coroutine start");
            TimeRemaining -= Time.deltaTime;
            //Debug.Log("deltaTime => " + Time.deltaTime.ToString());
            //Debug.Log(TimeRemaining / _timer);
            _slider.value = TimeRemaining / _timer;
            _sliderImage.color = _gradient.Evaluate(_slider.value);
            //Debug.Log("slider = " + (TimeRemaining / _timer));
            yield return null;
        }
        if(TimeRemaining <= 0) 
        { 
            OnTimerFinished?.Invoke(this);
            yield return null;
        }
        yield return null;
    }
}
