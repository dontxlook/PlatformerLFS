using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Components
{
    public class DoInteractComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject gameObject)
        {
            var interactable = gameObject.GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}

