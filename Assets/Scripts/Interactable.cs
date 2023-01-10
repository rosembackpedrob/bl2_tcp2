using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{   
    //Adiciona ou remove InteractionEvent desse gameobject.
    public bool useEvents;
    //mensagem de interação
    public string promptMessage;

    //método vai ser chamado pelo script do Player.
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvents>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //função vazia para ser sobrescrevida.
    }
}
