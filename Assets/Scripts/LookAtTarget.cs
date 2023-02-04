using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public GameObject target;

    public bool ifRotate = true;
    public float rotationSpeed = 5f;
    public bool ifRotateSlerp = false;
    public bool ifRotateLerp = false;

    //Rotation Axis
    public bool XAxis = true;
    public bool YAxis = false;
    public bool ZAxis = true;

    void Start ()
    {
        if (target == null)
            Debug.LogWarning("Target not declared in inspector");
    }
	
	void LateUpdate ()
    {
        RotateInTargetView();
    }
	
	public void SetTarget (GameObject _target)
    {
        target = _target;
    }

    public void RotateInTargetView()
    {
        if (ifRotate)
            transform.rotation = Quaternion.LookRotation(GetViewAxis() - transform.position);
        if (ifRotateSlerp && rotationSpeed > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GetViewAxis() - transform.position), rotationSpeed * Time.deltaTime);
        else if (ifRotateLerp && rotationSpeed > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetViewAxis() - transform.position), rotationSpeed * Time.deltaTime);
    }

    Vector3 GetViewAxis()
    {
        Vector3 returnedVector = new Vector3();

        if (XAxis) { returnedVector.x = target.transform.position.x; }
        else { returnedVector.x = this.transform.position.x; }
        if (YAxis) { returnedVector.y = target.transform.position.y; }
        else { returnedVector.y = this.transform.position.y; }
        if (ZAxis) { returnedVector.z = target.transform.position.z; }
        else { returnedVector.z = this.transform.position.z; }

        return returnedVector;
    }

#if UNITY_EDITOR
    public void RotateFromEditor() //called from Editor
    {
        transform.rotation = Quaternion.LookRotation(GetViewAxis() - transform.position);
    }

    public void ResetRotationFromEditor() //called from Editor
    {
        transform.rotation = Quaternion.identity;
    }
#endif

}
