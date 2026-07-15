using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Image energyImage;
    public Sprite[] energySprites;
    public int maxEnergy = 10;

    public int currentEnergy;

    private void Start()
    {
        currentEnergy = 0;
        RefreshEnergy();
    }

    public void SetEnergy(int hp)
    {
        currentEnergy = Mathf.Clamp(hp, 0, maxEnergy);
        RefreshEnergy();
    }

    private void RefreshEnergy()
    {
        energyImage.sprite = energySprites[currentEnergy];
    }
}