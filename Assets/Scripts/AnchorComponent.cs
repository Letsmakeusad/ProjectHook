using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(LineRenderer))]
public class AnchorComponent : MonoBehaviour
{
    AnchorSystem m_anchorSystem; 
    DistanceJoint2D m_anchor;
    LineRenderer m_lineRenderer;
    Transform anchorIndicator;

    private void Start()
    {
        m_anchor = GetComponent<DistanceJoint2D>();
        m_lineRenderer = GetComponent<LineRenderer>();

        m_anchorSystem = AnchorSystem.GetInstance();
        m_anchorSystem.RegisterAnchor(this);

        if (m_lineRenderer != null) { m_lineRenderer.positionCount = 2; }

        anchorIndicator = this.transform.GetChild(0);
        anchorIndicator.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (m_lineRenderer != null && m_anchor.connectedBody != null)
        {
            m_lineRenderer.SetPosition(0, transform.position);
            m_lineRenderer.SetPosition(1, m_anchor.connectedBody.transform.position);
        }
    }

    public void AttachUser(IAnchorUser _user)
    {
        m_anchor.connectedBody = _user.GetRigidbody2D();
       
        Vector2 dir = (Vector2)transform.position - m_anchor.connectedBody.position;
        m_anchor.connectedBody.AddForce(dir * 50f);
        m_anchor.distance -= 0.5f;
        m_lineRenderer.enabled = true;

    }

    public bool IsAttachedToUser(IAnchorUser _user) { return m_anchor.connectedBody == _user.GetRigidbody2D(); }

    public void DetachUser()
    {
        m_anchor.connectedBody = null;
        m_lineRenderer.enabled = false;
    }

    public void MarkAnchor()
    {
        anchorIndicator.gameObject.SetActive(true);
    }

    public void UnmarkAnchor()
    {
        anchorIndicator.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        m_anchorSystem.UnregisterAnchor(this);
    }



}
