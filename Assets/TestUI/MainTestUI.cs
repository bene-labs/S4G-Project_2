using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MainTestUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;

    private bool open = false;

    public void SetUp(UIDocument doc)
    {
        document = doc;
        root = document.rootVisualElement;

        root.Q<Button>("Demo").clicked += OnStartPressed;
        root.Q<Button>("Close").clicked += OnClosePressed;
    }

    public void Show()
    {
        open = true;

        root.visible = true;

        root.Q<VisualElement>("Container").SetEnabled(true);
        root.Q<VisualElement>("Background").SetEnabled(true);
    }

    public void Hide()
    {
        open = false;

        root.Q<VisualElement>("Container").SetEnabled(false);
        root.Q<VisualElement>("Background").SetEnabled(false);

        root.Q<VisualElement>("Container").RegisterCallback<TransitionEndEvent>(AfterHide);
    }

    private void AfterHide(TransitionEndEvent evt)
    {
        root.Q<VisualElement>("Container").UnregisterCallback<TransitionEndEvent>(AfterHide);

        root.visible = true;
    }

    private void OnStartPressed()
    {
        Hide();
    }

    private void OnClosePressed()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            open = !open;

            if (open)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

}
