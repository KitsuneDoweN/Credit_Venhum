using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    private Transform m_trTarget;

    private float m_fDefalutZoom;

    private Transform trTarget
    {
        set
        {
            m_trTarget = value;
        }
        get
        {
            return m_trTarget;
        }
    }


    private Vector2 m_v2TargetPos;
    private Vector2 m_v2CurrentPos;
    private Vector2 m_v2CurrentVelocity;

    private float m_fTargetZoom;
    private float m_fCurrentZoom;
    private float m_fCurrentZoomVelocity;




    [SerializeField] private float m_fSmoothTime;
    [SerializeField] private Camera m_camCamera;




    private delegate void CameraEvent();

    private CameraEvent []m_delCameraEvent;



    private Vector2 m_v2BoundZero;
    private Vector2 m_v2BoundOne;
    public enum E_CameraState 
    { 
        E_NONE, E_TRACKING, E_TOTAL
    }

    private E_CameraState m_eState;


    public void init(Transform target)
    {
        m_delCameraEvent = new CameraEvent[(int)E_CameraState.E_TOTAL];

        m_delCameraEvent[(int)E_CameraState.E_NONE] = noneEvent;
        m_delCameraEvent[(int)E_CameraState.E_TRACKING] = trackingEvent;


        trTarget = target;

        m_v2TargetPos = (Vector2)trTarget.position;
        m_v2CurrentPos = m_v2TargetPos;

        m_fDefalutZoom = m_camCamera.orthographicSize;
        m_fCurrentZoom = m_fDefalutZoom;
        m_fTargetZoom = m_fCurrentZoom;

        setPos((Vector2)target.position);

        m_v2BoundZero = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.zero) - (Vector2)transform.position;

        setStateTracking(GameManager.instance.cStageManager.cPlayer.transform);
    }


    public void setTarget(Transform target)
    {
        trTarget = target;
    }

    private void currentPosUpdate(Vector2 v2TargetPos)
    {
        m_v2CurrentPos = Vector2.SmoothDamp(m_v2CurrentPos, v2TargetPos, ref m_v2CurrentVelocity, m_fSmoothTime );
    }

    private void tracking()
    {
        currentPosUpdate((Vector2)trTarget.position);
        setPos(m_v2CurrentPos);
    }

    private void zooming()
    {
        currentZoomUpdate(m_fTargetZoom);
        m_camCamera.orthographicSize = m_fCurrentZoom;
       
    }

    private void FixedUpdate()
    {
        m_delCameraEvent[(int)m_eState]();
    }


    private void setPos(Vector2 v2Pos)
    {
        transform.position = new Vector3(v2Pos.x, v2Pos.y, transform.position.z);
    }

    private void noneEvent()
    {

    }

    private void trackingEvent()
    {
        tracking();
        zooming();
    }






    public void setStateNone()
    {
        m_camCamera.orthographicSize = m_fDefalutZoom;
        setTargetZoom(m_fDefalutZoom);
        m_eState = E_CameraState.E_NONE;
    }

    public void setStateTracking(Transform trTarget)
    {
        setTarget(trTarget);
        setTargetZoom(m_fDefalutZoom);
        m_eState = E_CameraState.E_TRACKING;
        m_camCamera.orthographicSize = m_fDefalutZoom;
    }



    private void setTargetZoom(float fZoom)
    {
        m_fTargetZoom = fZoom;
    }


    private void currentZoomUpdate(float fZoom)
    {
        m_fCurrentZoom = Mathf.SmoothDamp(m_fCurrentZoom, m_fTargetZoom, ref m_fCurrentZoomVelocity, 0.5f);
    }

    private void updateBounds()
    {
        m_v2BoundZero = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.zero);
        m_v2BoundOne = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.one);
        Debug.Log(
            "Center " + m_camCamera.transform.position +
            "Zero " + m_v2BoundZero +
            "One " + m_v2BoundOne);
    }

}
