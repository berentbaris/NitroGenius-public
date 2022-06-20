using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIConsequenceTooltip : MonoBehaviour
{
    public Sprite imageSprite;
    public Sprite NH3Sprite;
    public Sprite NOxSprite;
    public Sprite N2OSprite;

    public Sprite adjust1;
    public Sprite adjust2;
    public Sprite adjust3;

    public Image icon;
    public Image amountIcon;

    public Color greenColor;
    public Color redColor;

    // name 0 = image
    // name 1 = NH3
    // name 2 = NOx
    // name 3 = N2O;
    public void DisplayConsequence(int name, string amount)
    {
        amountIcon.transform.rotation = Quaternion.Euler(1, 1, 1);
        icon.sprite = null;
        amountIcon.sprite = null;
        amountIcon.color = Color.white;

        switch (name)
        {
            case 0:
                icon.sprite = imageSprite;
                break;
            case 1:
                icon.sprite = NH3Sprite;
                break;
            case 2:
                icon.sprite = NOxSprite;
                break;
            case 3:
                icon.sprite = N2OSprite;
                break;
            default:
                break;
        }

        if (name == 0)
        {
            switch (amount)
            {
                case "+":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = greenColor;
                    break;
                case "++":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = greenColor;
                    break;
                case "+++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    break;
                case "++++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    break;
                case "-":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "--":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "---":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "----":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (amount)
            {
                case "+":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = redColor;
                    break;
                case "++":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = redColor;
                    break;
                case "+++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    break;
                case "++++":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = redColor;
                    break;
                case "-":
                    amountIcon.sprite = adjust1;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "--":
                    amountIcon.sprite = adjust2;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "---":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                case "----":
                    amountIcon.sprite = adjust3;
                    amountIcon.color = greenColor;
                    amountIcon.transform.Rotate(new Vector3(0, 0, 180));
                    break;
                default:
                    break;
            }
        }
    }
}