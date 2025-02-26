using UnityEditor;
using UnityEngine;

public class PauseSetter : MonoBehaviour
{
    public void SetPause()
    {
        EditorApplication.isPaused = true;
    }
}
