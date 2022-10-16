using UnityEngine;

public class CardUI : MonoBehaviour
{
    public string CardID;
    private bool isPreview;
    private bool isSelected;
    private bool isLocked;

    [SerializeField]
    private GameObject borderGO;

    [SerializeField]
    private GameObject selectGO;

    [SerializeField]
    private GameObject lockGO;

    public bool IsPreview
    {
        get
        {
            return isPreview;
        }
        set
        {
            if (value)
            {
                isPreview = true;
                borderGO.SetActive(true);
            }
            else
            {
                isPreview = false;
                borderGO.SetActive(false);
            }
        }
    }
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            if (value)
            {
                isSelected = true;
                selectGO.SetActive(true);
            }
            else
            {
                isSelected = false;
                selectGO.SetActive(false);
            }
        }
    }
    public bool IsLocked 
    {
        get
        {
            return isLocked;
        }
        set 
        {
            if (value)
            {
                isLocked = true;
                lockGO.SetActive(true);
            }
            else
            {
                isLocked = false;
                lockGO.SetActive(false);
            }
        } 
    }
}
