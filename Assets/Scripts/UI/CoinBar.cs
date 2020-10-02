using UnityEngine;
using UnityEngine.UI;

public class CoinBar : MonoBehaviour
{
    [SerializeField] private Text _text;    
    public void UpdateCoins(int numOfCoins)
    {
        _text.text = numOfCoins.ToString();
    }
}
