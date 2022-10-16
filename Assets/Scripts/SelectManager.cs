using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private Transform characterAnchor;

    [SerializeField]
    private Transform platformAnchor;

    [SerializeField]
    private CardUI cardPrefab;

    [SerializeField]
    private Page characterPage;

    [SerializeField]
    private Page platformPage;

    [SerializeField]
    private TextMeshProUGUI itemNameText;

    [SerializeField]
    private Character[] characterPrefabs;

    [SerializeField]
    private Platform[] platformPrefabs;

    public Character SelectedCharacter { get; private set; }
    public Character CurrentCharacter { get; private set; }

    public Platform SelectedPlatform { get; private set; }
    public Platform CurrentPlatform { get; private set; }

    private void Awake()
    {
        SpawnCards();
        Load();
    }

    public void PreviewCharacter(Character characterToPreview)
    {
        if (CurrentCharacter != null)
        {
            if (CurrentCharacter == characterToPreview)
                return;
            Destroy(CurrentCharacter.gameObject);
        }

        CurrentCharacter = Instantiate(characterToPreview, characterAnchor);
        characterAnchor.GetComponent<Animator>().Play("Character_jumpOut_01", -1, 0f);
        itemNameText.text = CurrentCharacter.CharacterName;
    }

    public void PreviewPlatform(Platform platformToPreview)
    {
        if (CurrentPlatform != null)
        {
            if (CurrentCharacter == platformToPreview)
                return;
            Destroy(CurrentPlatform.gameObject);
        }

        CurrentPlatform = Instantiate(platformToPreview, platformAnchor);
        itemNameText.text = CurrentPlatform.PlatformName;
    }

    public void ShowItemName(Page page)
    {
        if (page == characterPage)
            itemNameText.text = CurrentCharacter.CharacterName;
        if (page == platformPage)
            itemNameText.text = CurrentPlatform.PlatformName;
    }


    public void Select()
    {
        if (characterPage.gameObject.activeSelf)
        {
            if (!CurrentCharacter.IsLocked)
            {
                SelectedCharacter = CurrentCharacter;
                characterPage.Select();
                PlayerPrefs.SetString("CharacterID", SelectedCharacter.CharacterID);
            }

        }
        if (platformPage.gameObject.activeSelf)
        {
            if (!CurrentPlatform.IsLocked)
            {
                SelectedPlatform = CurrentPlatform;
                platformPage.Select();
                PlayerPrefs.SetString("PlatformID", SelectedPlatform.PlatformID);
            }
        }
    }

    private void SpawnCards()
    {
        platformPage.CardUIs = new CardUI[platformPrefabs.Length];
        for (int i = 0; i < platformPrefabs.Length; i++)
        {
            int value = i;
            CardUI card = Instantiate(cardPrefab, platformPage.transform);
            card.CardID = platformPrefabs[value].PlatformID;
            if (platformPrefabs[value].IsLocked)
                card.IsLocked = true;
            card.transform.Find("Image").GetComponent<Image>().sprite = platformPrefabs[value].Sprite;
            card.GetComponent<Button>().onClick.AddListener(() => PreviewPlatform(platformPrefabs[value]));
            card.GetComponent<Button>().onClick.AddListener(() => platformPage.Preview(card.GetComponent<CardUI>()));
            platformPage.CardUIs[value] = card.GetComponent<CardUI>();
        }

        characterPage.CardUIs = new CardUI[characterPrefabs.Length];
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            int value = i;
            CardUI card = Instantiate(cardPrefab, characterPage.transform);
            card.CardID = characterPrefabs[value].CharacterID;
            if (characterPrefabs[value].IsLocked)
                card.IsLocked = true;
            card.transform.Find("Image").GetComponent<Image>().sprite = characterPrefabs[value].Sprite;
            card.GetComponent<Button>().onClick.AddListener(() => PreviewCharacter(characterPrefabs[value]));
            card.GetComponent<Button>().onClick.AddListener(() => characterPage.Preview(card.GetComponent<CardUI>()));
            characterPage.CardUIs[i] = card.GetComponent<CardUI>();
        }
    }

    private void Load()
    {
        var characterID = PlayerPrefs.GetString("CharacterID");
        var platformID = PlayerPrefs.GetString("PlatformID");

        if (string.IsNullOrEmpty(characterID))
        {
            PreviewCharacter(characterPrefabs[0]);
            SelectedCharacter = characterPrefabs[0];

            characterPage.Preview(characterPage.CardUIs[0]);
            characterPage.Select();
        }
        else
        {
            for (int i = 0; i < characterPrefabs.Length; i++)
            {
                if (characterID == characterPrefabs[i].CharacterID)
                {
                    PreviewCharacter(characterPrefabs[i]);
                    SelectedCharacter = characterPrefabs[i];
                    characterPage.Preview(characterPage.CardUIs[i]);
                    characterPage.Select();
                    break;
                }
            }
        }

        if (string.IsNullOrEmpty(platformID))
        {
            PreviewPlatform(platformPrefabs[0]);
            SelectedPlatform = platformPrefabs[0];

            platformPage.Preview(platformPage.CardUIs[0]);
            platformPage.Select();

            return;
        }
        else
        {
            for (int i = 0; i < platformPrefabs.Length; i++)
            {
                if (platformID == platformPrefabs[i].PlatformID)
                {
                    PreviewPlatform(platformPrefabs[i]);
                    SelectedPlatform = platformPrefabs[i];
                    platformPage.Preview(platformPage.CardUIs[i]);
                    platformPage.Select();
                    break;
                }
            }
        }
    }
}
