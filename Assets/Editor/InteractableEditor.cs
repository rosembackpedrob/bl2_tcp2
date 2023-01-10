using UnityEditor;

[CustomEditor(typeof(Interactable), true)]

public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField
                    ("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable só usa UnityEvents",MessageType.Info);
            if(interactable.GetComponent<InteractionEvents>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvents>();
            }
        }
        else 
        {
            base.OnInspectorGUI();

            if(interactable.useEvents)
            {
                if(interactable.GetComponent<InteractionEvents>() == null)
                    interactable.gameObject.AddComponent<InteractionEvents>();
            
            }
            else
            {
                //Não usando eventos, remover componente.
                if(interactable.GetComponent<InteractionEvents>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvents>());    
            }
        }
    }
}
