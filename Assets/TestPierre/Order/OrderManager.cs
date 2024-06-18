using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderInfo orderPrefab;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private Transform OrderListTransform;
    [SerializeField] private int maxOrders;

    private int OrderCount = 1;
    private float elapsedTime = 0f;
    private List<OrderInfo> orderList = new();
    public List<OrderInfo> OrderList => orderList;


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > SpawnTimer)
        {
            elapsedTime = 0;
            if (orderList.Count >= maxOrders) return;
            var order = Instantiate(orderPrefab, OrderListTransform);
            orderList.Add(order);
            //order.OnTimerFinished += LateOrder;
            order.OrderNumber = OrderCount;
            OrderCount++;
        }
    }
    
    //private void LateOrder(OrderInfo order)
    //{
    //    Debug.Log("finish timer");
    //    orderList.Remove(order);
    //}
}
