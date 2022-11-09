using UnityEngine;
using UnityEngine.Events;

public class EntityMana : AmountInABar
{
    private void Start()
    {
        Initialize();
        Bar.SetBar(Value);    
    }
}
