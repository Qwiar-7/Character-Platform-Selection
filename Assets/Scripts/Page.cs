using UnityEngine;

public class Page : MonoBehaviour
{
    public string Name;
    public CardUI[] CardUIs;
    public CardUI SelectedItem { get; private set; }
    public CardUI PreviewItem { get; private set; }

    public virtual void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void Preview(CardUI card)
    {
        if(PreviewItem == card)
            return;

        foreach (var item in CardUIs)
        {
            if(item == card)
            {
                if (PreviewItem != null)
                    PreviewItem.IsPreview = false;

                item.IsPreview = true;
                PreviewItem = item;
            }
        }
    }

    public void Select()
    {
        if(PreviewItem == null || PreviewItem.IsSelected)
            return;

        if (SelectedItem != null)
            SelectedItem.IsSelected = false;

        PreviewItem.IsSelected = true;
        SelectedItem = PreviewItem;
    }
}
