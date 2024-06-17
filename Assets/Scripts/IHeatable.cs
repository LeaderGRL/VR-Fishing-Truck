using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeatable
{
    int heatTime { get; set; }
    bool isCooked { get; set; }
    void Heat();
}
