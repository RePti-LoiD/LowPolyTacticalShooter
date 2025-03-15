using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button; //� ��� ���� � ���������� ������������� �� �������� ������ ������ 

    private void OnEnable()
    {
        button.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ExitGame);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif     
        Application.Quit();
    }
}
