/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// 
/// Changes made to this file could be overwritten when upgrading the Vuforia version. 
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    public GameObject[] models3D;

    public GameObject canvasGame;
    
    public GameObject[] desc;
    public AudioSource[] voiceOver;
    public GameObject[] otherButton;

    bool rotRight = false;
    bool rotLeft = false;

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
            canvasGame.SetActive(true);
            models3D[0].SetActive(true);
            models3D[1].SetActive(true);
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
            canvasGame.SetActive(false);
            models3D[0].SetActive(false);
            models3D[1].SetActive(false);
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
            canvasGame.SetActive(false);
            models3D[0].SetActive(false);
            models3D[1].SetActive(false);
        }
    }

    public void OpenDesc(int index)
    {
        desc[index].SetActive(true);
        for (int i = 0; i < otherButton.Length; i++)
        {
            otherButton[i].SetActive(false);
        }
        //buttonSound.SetActive(false);
        voiceOver[index].Play();

    }
    public void ExitDesc(int index)
    {
        desc[index].SetActive(false);
        for (int i = 0; i < otherButton.Length; i++)
        {
            otherButton[i].SetActive(true);
        }
        //buttonSound.SetActive(true);
        voiceOver[index].Stop();
    }

    void Update()
    {
        if (rotRight)
        {
            for (int i = 0; i < models3D.Length; i++)
            {
                models3D[i].transform.Rotate(Vector3.up, 50f * Time.deltaTime);
            }
        } else if (rotLeft)
        {
            for (int i = 0; i < models3D.Length; i++)
            {
                models3D[i].transform.Rotate(Vector3.down, 50f * Time.deltaTime);
            }
        }
    }

    public void RotateRightPressed()
    {
        rotRight = true;
    }
    public void RotateRightReleased()
    {
        rotRight = false;
    }

    public void RotateLeftPressed()
    {
        rotLeft = true;
    }
    public void RotateLeftReleased()
    {
        rotLeft = false;
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PROTECTED_METHODS
}
