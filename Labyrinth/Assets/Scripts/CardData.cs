using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardDataSO", order = 1)]
public class CardData : ScriptableObject
{
    public int[] ids = new int[24];
    public string[] cardText = new string[24];
}