using UnityEngine;

[CreateAssetMenu(fileName = "Create interactabel data asset", menuName = "Scriptable Objects/InteractableData")] 
public class InteractableData : ScriptableObject
{
    [SerializeField] private string interactableName;
    [SerializeField] private bool pickable;

    public string InteractableName { get => interactableName; }
    public bool Pickable { get => pickable; }
}
