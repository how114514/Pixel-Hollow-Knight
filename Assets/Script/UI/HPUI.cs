using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image hpImage;
    public Sprite[] hpSprites;
    public int maxHP = 10;

    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
        RefreshHP();
    }

    public void SetHP(int hp)
    {
        currentHP = Mathf.Clamp(hp, 0, maxHP);
        RefreshHP();
    }

    private void RefreshHP()
    {
        hpImage.sprite = hpSprites[currentHP];
    }
}