using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    public void ActiveObject(int objIndex)
    {
        objects[objIndex].SetActive(true);
    }

    public void DesactiveObject(int objIndex)
    {
        objects[objIndex].SetActive(false);
    }
}
