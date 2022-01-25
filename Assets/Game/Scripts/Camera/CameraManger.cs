using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour, IUpdate
{
    [SerializeField]
    private CameraClamp m_CamClamp;
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

    [SerializeField] private float m_fUnitTrackingDistance = 1.5f;


    [SerializeField] private float m_fSmoothTime;
    [SerializeField] private Camera m_camCamera;


    private delegate void CameraEvent();

    private CameraEvent []m_delCameraEvent;

    private UnitBase m_cUnit;

    private Vector2 m_v2BoundZero;
    private Vector2 m_v2BoundOne;
    public enum E_CameraState 
    { 
        E_NONE, E_TRACKING, E_TRACKING_UNIT, E_FIX ,E_TOTAL
    }

    private E_CameraState m_eState;




    public void init(Transform target)
    {
        m_delCameraEvent = new CameraEvent[(int)E_CameraState.E_TOTAL];

        m_delCameraEvent[(int)E_CameraState.E_NONE] = noneEvent;
        m_delCameraEvent[(int)E_CameraState.E_TRACKING] = trackingEvent;
        m_delCameraEvent[(int)E_CameraState.E_TRACKING_UNIT] = trackingUnitEvent;

        trTarget = target;

        m_v2TargetPos = (Vector2)trTarget.position;
        m_v2CurrentPos = m_v2TargetPos;

        m_fDefalutZoom = m_camCamera.orthographicSize;
        m_fCurrentZoom = m_fDefalutZoom;
        m_fTargetZoom = m_fCurrentZoom;

        m_CamClamp.init();


        setPos((Vector2)target.position);

        m_v2BoundZero = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.zero) - (Vector2)transform.position;


        setStateTrackingUnit(GameManager.Instance.cStageManager.cPlayer);

        processedBounds();

        UpdateManager.Instance.addProcesses(this);
    }


    public void setTarget(Transform target)
    {
        trTarget = target;
    }

    private void currentPosUpdate(Vector2 v2TargetPos)
    {


        m_v2CurrentPos = Vector2.SmoothDamp(m_v2CurrentPos, v2TargetPos, ref m_v2CurrentVelocity, m_fSmoothTime );
    }

    private void tracking(Vector2 targetPos)
    {
        currentPosUpdate(targetPos);
        setPos(m_v2CurrentPos);
    }

    private void zooming()
    {
        currentZoomUpdate(m_fTargetZoom);
        m_camCamera.orthographicSize = m_fCurrentZoom;
       
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
        tracking((Vector2)trTarget.position);
        zooming();

        processedBounds();

        
    }

    private void trackingUnitEvent()
    {
        Vector2 v2UnitLookPoint = (Vector2)trTarget.position + (m_cUnit.v2LookDir * m_fUnitTrackingDistance);

        tracking(v2UnitLookPoint);
        zooming();

        processedBounds();
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

    public void setStateTrackingUnit(UnitBase unit)
    {
        m_cUnit = unit;

        setTarget(m_cUnit.transform);
        setTargetZoom(m_fDefalutZoom);
        m_eState = E_CameraState.E_TRACKING_UNIT;
        m_camCamera.orthographicSize = m_fDefalutZoom;
    }

    public void setStatePivot(Transform trPivot, float fZoom)
    {
        setTarget(trPivot);
        setTargetZoom(fZoom);

        m_eState = E_CameraState.E_TRACKING;
    }



    private void setTargetZoom(float fZoom)
    {
        m_fTargetZoom = fZoom;
    }


    private void currentZoomUpdate(float fZoom)
    {
        m_fCurrentZoom = Mathf.SmoothDamp(m_fCurrentZoom, m_fTargetZoom, ref m_fCurrentZoomVelocity, 0.5f);
    }


    private void processedBounds()
    {
        m_v2BoundZero = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.zero);
        m_v2BoundOne = (Vector2)m_camCamera.ViewportToWorldPoint(Vector2.one);
       
        //Debug.Log(
        //    "Center " + m_camCamera.transform.position +
        //    "Zero " + m_v2BoundZero +
        //    "One " + m_v2BoundOne);



        m_v2CurrentPos = m_CamClamp.clamp(
    (Vector2)transform.position,
    m_v2BoundZero,
    m_v2BoundOne);

        setPos(m_v2CurrentPos);

    }

    public void updateProcesses()
    {
        if (GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME)
            m_delCameraEvent[(int)m_eState]();
    }



    public Camera mainCam
    {
        get
        {
            return m_camCamera;
        }
    }

    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.removeProcesses(this);
    }
}
