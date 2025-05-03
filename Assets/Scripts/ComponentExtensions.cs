using UnityEngine;

public static class ComponentExtensions
{
    public static bool TryGetComponentInParent<T>(this Component component, out T result) where T : Component
    {
        result = component.GetComponentInParent<T>();
        return result != null;
    }

    public static bool TryGetInterfaceInParent<T>(this Component component, out T result) where T : class
    {
        result = null;
        Component current = component;

        while (current != null)
        {
            if (current.TryGetComponent(out Component found) && found is T interfaceComponent)
            {
                result = interfaceComponent;
                return true;
            }
            current = current.transform.parent != null ? current.transform.parent.GetComponent<Component>() : null;
        }

        return false;
    }
}
