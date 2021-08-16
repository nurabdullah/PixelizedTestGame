using UnityEngine;

public enum BoostType
{
    Add,
    Substract,
    Multiply,
    Divide
}

public class Booster : MonoBehaviour
{
    public BoostType boostType;
    public int count;
}
