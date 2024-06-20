using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderInfo orderPrefab;
    [SerializeField] private float SpawnTimerMin;
    [SerializeField] private float SpawnTimerMax;
    [SerializeField] private Transform OrderListTransform;
    [SerializeField] private int maxOrders;
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private AudioSource audioSuccess;
    [SerializeField] private AudioSource audioFailure;

    private int OrderCount = 1;
    private float elapsedTime = 0f;
    private List<OrderInfo> orderList = new();
    public List<OrderInfo> OrderList => orderList;
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = SpawnTimerMin;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > spawnTimer)
        {
            elapsedTime = 0;
            if (orderList.Count >= maxOrders) return;
            var order = Instantiate(orderPrefab, OrderListTransform);
            orderList.Add(order);
            //order.OnTimerFinished += LateOrder;
            order.OrderNumber = OrderCount;
            OrderCount++;
            spawnTimer = Random.Range(SpawnTimerMin, SpawnTimerMax);
        }
    }
    
    public void ValidateOrder(OrderType typeOrder)
    {
        foreach(var order in orderList)
        {
            if(order.TypeOrder == typeOrder)
            {
                foreach (var particle in particleSystems)
                {
                    particle.Play();
                }
                audioSuccess.Play();
                order.ValidateOrder();
                orderList.Remove(order);
                return;
            }
        }
        audioFailure.Play();
    }
    //private void LateOrder(OrderInfo order)
    //{
    //    Debug.Log("finish timer");
    //    orderList.Remove(order);
    //}
}
