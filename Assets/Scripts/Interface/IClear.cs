using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClear
{
    public ClearTrigger Trigger { get; set; }
    public void Clear();
}
