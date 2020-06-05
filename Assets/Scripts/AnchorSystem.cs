using System.Collections.Generic;
using UnityEngine;

public class AnchorSystem : Singleton<AnchorSystem>
{
    HashSet<AnchorComponent> m_anchorComponentList = new HashSet<AnchorComponent>();

    public void RegisterAnchor(AnchorComponent _component) { m_anchorComponentList.Add(_component); }
    public void UnregisterAnchor(AnchorComponent _component) { m_anchorComponentList.Remove(_component); }

    public void AttachToNearestAnchor(IAnchorUser _anchorUser)
    {
        AnchorComponent nearestAnchor = FindNearestAnchorComponent(_anchorUser.GetRigidbody2D().transform.position);
        if (nearestAnchor != null)
        {
            nearestAnchor.AttachUser(_anchorUser);
        }
        else Debug.LogWarning("No anchors found to attach the body");
    }

    public void Detach(IAnchorUser _user)
    {
        AnchorComponent attachedAnchor = null;

        foreach (AnchorComponent _anchor in m_anchorComponentList)
        {
            if (_anchor.IsAttachedToUser(_user)) { attachedAnchor = _anchor; }
        }
        if (attachedAnchor != null) { attachedAnchor.DetachUser(); }
    }

    public AnchorComponent FindNearestAnchorComponent(Vector2 _position)
    {
        float shortestDistance = Mathf.Infinity;
        AnchorComponent nearestAnchor = null;
        
        foreach (AnchorComponent _component in m_anchorComponentList)
        {
            float distance = (_position - (Vector2)_component.transform.position).sqrMagnitude;
            if (distance < shortestDistance)
            {
                foreach (var anchor in m_anchorComponentList)
                {
                    anchor.UnmarkAnchor();
                }
                shortestDistance = distance;             
                nearestAnchor = _component;
                nearestAnchor.MarkAnchor();

            }
            

        }

        return nearestAnchor;
    }
}

