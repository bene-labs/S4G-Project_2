using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument document;
    [SerializeField]
    private MainTestUI mainTestUI;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        mainTestUI = GetComponent<MainTestUI>();

        mainTestUI.SetUp(document);

        mainTestUI.Show();
    }

   
}
