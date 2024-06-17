using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderInfo orderPrefab;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private Transform OrderList;

    private int OrderCount = 1;
    private float elapsedTime = 0f;
    private List<OrderInfo> orderList = new();


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > SpawnTimer)
        {
            elapsedTime = 0;
            var order = Instantiate(orderPrefab, OrderList);
            orderList.Add(order);
            order.OnTimerFinished += LateOrder;
            order.OrderNumber = OrderCount;
            OrderCount++;
        }
    }
    
    private void LateOrder(OrderInfo order)
    {
        Debug.Log("finish timer");
        orderList.Remove(order);
    }
}
