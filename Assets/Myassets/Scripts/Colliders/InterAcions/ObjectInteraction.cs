using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.InputSystem;

public class ObjectInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject TextBoxGO;
    [SerializeField] GameObject arrowGO;
    [SerializeField] TextMeshProUGUI TMPmessage;
    TMP_TextInfo messageTextInfo;
    [SerializeField] Interact interactScript;

    [SerializeField] string[] textArray;

    [Range(0, 200)]
    [SerializeField] int currentVisibleChars;
    float visibleCharFloat;

    [SerializeField] float TypeWritingSpeed = 1;


    [Header("Debug")]
    [SerializeField] float MaxVisibleChars;
    [SerializeField] int currentStringLength;
    [SerializeField] int textArrayIndex = 0;
    [SerializeField] int textArrayLength;
    [SerializeField] char currentLastVisibleChar;
    [SerializeField] bool isAdvancingText;


    // Start is called before the first frame update
    void Start()
    {
        TextBoxGO.SetActive(false);
        arrowGO.SetActive(false);
        textArrayLength = textArray.Length;
        messageTextInfo = TMPmessage.textInfo;

    }

    void Update()
    {
        if (isAdvancingText == true)
            TypeWriterTextAdvance();

    }

    public System.Action<InputAction.CallbackContext> InitInteractionFunction()
    {
        TextBoxGO.SetActive(true);
        TMPmessage.text = textArray[0];

        TypeWritertextStart();

        return ReadSign;
    }

    public void ReadSign(InputAction.CallbackContext ctx)
    {
        if (isAdvancingText == true)
        {
            SkipTextToEnd();
            return;
        }

        textArrayIndex += 1;

        if (textArrayIndex >= textArrayLength)
        {
            End();
            textArrayIndex = 0;
            return;
        }

        TMPmessage.text = textArray[textArrayIndex];
        TypeWritertextStart();
    }

    void TypeWriterTextAdvance()
    {
        visibleCharFloat += Time.deltaTime * TypeWritingSpeed;
        currentVisibleChars = Mathf.FloorToInt(visibleCharFloat);
        TMPmessage.maxVisibleCharacters = currentVisibleChars;

        if (currentStringLength + 1 < currentVisibleChars)
        {
            isAdvancingText = false;
            arrowGO.SetActive(true);
        }
    }

    void TypeWritertextStart()
    {
        isAdvancingText = true;
        TMPmessage.maxVisibleCharacters = 0;
        visibleCharFloat = 0;
        currentStringLength = textArray[textArrayIndex].Length;
        arrowGO.SetActive(false);
    }

    void SkipTextToEnd()
    {
        isAdvancingText = false;
        TMPmessage.maxVisibleCharacters = currentStringLength;
        arrowGO.SetActive(true);
    }

    public void End()
    {
        TextBoxGO.SetActive(false);
        InputManager.ResetInteractionFunction();
    }
}
