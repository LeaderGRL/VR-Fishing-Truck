
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfo : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _foodImage;
    [SerializeField] private TextMeshProUGUI _orderNumber;
    [SerializeField] private TextMeshProUGUI _orderTextInfo;
    [SerializeField] private float _timer = 30f;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderImage;
    [SerializeField] private int _rewardPoints;
    [SerializeField] private int _lateRewardPoints;
    [SerializeField] private Sprite _image1;
    [SerializeField] private Sprite _image2;
    [SerializeField] private Sprite _image3;
    // Start is called before the first frame update

    private OrderType typeOrder;
    public OrderType TypeOrder => typeOrder;
    public int RewardPoints => _rewardPoints;
    public Gradient Gradient => _gradient;
    public int OrderNumber = -1;
    public float TimeRemaining { get; private set; }

    //public event Action<OrderInfo> OnTimerFinished;

    private void Start()
    {
        TimeRemaining = _timer;
        Debug.Log("spawn => " + TimeRemaining.ToString());
        _orderNumber.text = OrderNumber.ToString();
        StartCoroutine(UpdateTimer());
        int random = Random.Range(0, 100);
        if (random < 47)
        {
            _foodImage.sprite = _image1;
            _orderTextInfo.text = "Poisson cru";
            typeOrder = OrderType.RawFish;
        }
        else if(random < 94)
        {
            _foodImage.sprite = _image2;
            _orderTextInfo.text = "Poisson cuit";
            typeOrder = OrderType.CookedFish;
        }
        else
        {
            _foodImage.sprite = _image3;
            _orderTextInfo.text = "Une Botte ???";
            typeOrder = OrderType.Boot;
        }
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
            //OnTimerFinished?.Invoke(this);
            _rewardPoints = _lateRewardPoints;
            yield return null;
        }
        yield return null;
    }

    public void ValidateOrder()
    {
        StartCoroutine(Validate());
    }
    public IEnumerator Validate()
    {
        _backgroundImage.color = Color.green;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}

public enum OrderType
{
    CookedFish, RawFish, Boot, None
}