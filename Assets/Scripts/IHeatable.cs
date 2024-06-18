using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeatable
{
    int HeatTime { get; set; }
    bool IsCooked { get; set; }
    void Heat();
}
