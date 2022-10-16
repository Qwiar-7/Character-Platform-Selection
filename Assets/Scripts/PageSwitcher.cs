using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageSwitcher : MonoBehaviour
{
    [SerializeField]
    private Page startPage;

    [SerializeField]
    private ScrollRect scrollPanel;

    [SerializeField]
    private TextMeshProUGUI groupNameText;
    
    [SerializeField]
    private SelectManager characterManager;

    private Page currentPage;

    private void Start()
    {
        SelectPage(startPage);
    }
    public void SelectPage(Page page)
    {
        currentPage?.SetActive(false);
        page.SetActive(true);
        currentPage = page;
        scrollPanel.content = currentPage.GetComponent<RectTransform>();
        groupNameText.text = currentPage.Name;
        characterManager.ShowItemName(page);
    }
}
