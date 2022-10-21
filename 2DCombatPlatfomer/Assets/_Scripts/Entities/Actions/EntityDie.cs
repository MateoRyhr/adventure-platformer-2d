using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDie : MonoBehaviour
{
    [SerializeField] EntityStatus2D entityStatus;
    [SerializeField] private int CorpseMask;

    public void Die(){
        entityStatus.gameObject.layer = CorpseMask;
    }
}
