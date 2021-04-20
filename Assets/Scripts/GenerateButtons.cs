using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject NumberButton;
    [SerializeField]
    private Text Input;
    [SerializeField]
    private string Answer;

    private GridLayoutGroup _gridGroup;
    
    void Start()
    {
        _gridGroup = GetComponent<GridLayoutGroup>();

        for(int i = 1; i < 10; i++)
        {

            GameObject btn = Instantiate(NumberButton);
            Button btnTrue = btn.GetComponent<Button>();
            Text btnText = btn.GetComponentInChildren<Text>();

            btnText.text = i.ToString();

            btn.transform.SetParent(_gridGroup.transform, false);

            int j = i; // Dereference

            btnTrue.onClick.AddListener(() =>
            {
                Input.text += j;
            });
        }
    }
}
