using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.InputSystem;
using System;

public enum TextStyles
{
    Item, Normal
}

public class TextBox : MonoBehaviour
{
    [SerializeField] GameObject TextBoxGO;
    [SerializeField] GameObject arrowGO;
    [SerializeField] TextMeshProUGUI TMPmessage;
    [SerializeField] TextMeshProUGUI normalMessageData;
    [SerializeField] TextMeshProUGUI itemMessageData;
    TMP_TextInfo messageTextInfo;

    string[] textArray;
    int typeWritingFasterSpeed = 8;

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

    public bool HasSkipFunction = false;
    float originalTypeWritingSpeed;

    TextStyles TextStyles = new TextStyles();

    void OnDisable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber -= UpdateText;
    }

    // Start is called before the first frame update
    void Start()
    {
        TextBoxGO.SetActive(false);
        arrowGO.SetActive(false);
        messageTextInfo = TMPmessage.textInfo;
    }

    void UpdateText()
    {
        TypeWriterTextAdvance();

        print("updatng text");
    }

    public void InitTextBox(float InputTypeSpeed, int InputFasterTypingSped, string[] InputTextArray, TextStyles inputStyle)
    {
        if (InputTextArray.Length == 0)
        {
            Debug.LogError("Empty Array passed");
            return;
        }


        TextBoxGO.SetActive(true);
        textArray = InputTextArray;

        TMPmessage.text = textArray[0];
        textArrayLength = InputTextArray.Length;
        TypeWritingSpeed = InputTypeSpeed;
        typeWritingFasterSpeed = InputFasterTypingSped;
        originalTypeWritingSpeed = InputTypeSpeed;

        SetTextStyle(inputStyle);
        TypeWritertextStart();
    }

    public void ContinueText()
    {
        if (isAdvancingText == true)
        {
            TypeWritingSpeed *= typeWritingFasterSpeed;
            return;
        }

        textArrayIndex += 1;

        if (textArrayIndex >= textArrayLength)
        {
            End();
            return;
        }


        TMPmessage.text = textArray[textArrayIndex];
        TypeWritertextStart();
    }

    public void SkipText()
    {
        if (isAdvancingText == false)
        {
            textArrayIndex += 1;


            if (textArrayIndex >= textArrayLength)
            {
                End();
                return;
            }

            TMPmessage.text = textArray[textArrayIndex];
            TypeWritertextStart();
        }

        else
        {
            SkipTextToEnd();
        }
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
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateText;
        }
    }

    void TypeWritertextStart()
    {
        CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateText;
        isAdvancingText = true;
        TMPmessage.maxVisibleCharacters = 0;
        visibleCharFloat = 0;
        currentStringLength = textArray[textArrayIndex].Length;
        arrowGO.SetActive(false);
        TypeWritingSpeed = originalTypeWritingSpeed;
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
        textArrayIndex = 0;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateText;
    }

    private void ChangeTextStyle(TextMeshProUGUI inputStyle)
    {
        TMPmessage.alignment = inputStyle.alignment;
        TMPmessage.fontStyle = inputStyle.fontStyle;
    }

    void SetTextStyle(TextStyles inputStyle)
    {
        switch (TextStyles)
        {
            case TextStyles.Item:

                ChangeTextStyle(itemMessageData);

                break;

            case TextStyles.Normal:

                ChangeTextStyle(normalMessageData);
                break;
        }

    }
}
