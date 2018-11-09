﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler {

    public GameObject ARTextNotification;

    void Awake()
    {
        ARTextNotification = GameObject.Find("ARNotificationText");
        ARTextNotification.SetActive(false);
    }

	public override void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            int index = 3;
            Debug.Log(index);
            index = PlayerInfo.ProcurarPista(mTrackableBehaviour.TrackableName);
            Debug.Log(index);
            PlayerInfo.DescobrirPista(index);
            //É aqui onde as funções ProcurarPistas e DescobrirPista serão chamadas
            //Antes disso será feita uma verificação pra testar se a pista já foi descoberta
            OnTrackingFound();
            ARTextNotification.SetActive(true);

        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
            ARTextNotification.SetActive(false);
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
            ARTextNotification.SetActive(false);
        }
    }
}
