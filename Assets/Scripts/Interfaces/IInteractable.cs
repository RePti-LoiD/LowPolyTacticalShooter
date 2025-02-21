using System;

public interface IInteractable
{
    public void Select();
    public void Unselect();

    public void Interact(object sender, EventArgs args);
    public InteractableData GetData();
}