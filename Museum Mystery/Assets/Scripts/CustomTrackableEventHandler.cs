using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler {

    public int etapaTarget;

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
            if (PlayerInfo.etapaAtual>=etapaTarget)
            {
                string targetName = mTrackableBehaviour.TrackableName;
                Debug.Log("Trackable " + targetName + " found");
                int index;

                targetName = targetName.Remove(targetName.Length-1);     //qdo os targets forem modificados lembrar de habilitar essa linha para remover o último caractere.
                Debug.Log("string targetName: "+targetName);
                index = PlayerInfo.ProcurarPista(targetName);
                Debug.Log(index);
                if(index != -1)
                {
                    PlayerInfo.DescobrirPista(index, true);
                }
                //É aqui onde as funções ProcurarPistas e DescobrirPista serão chamadas
                //Antes disso será feita uma verificação pra testar se a pista já foi descoberta
                OnTrackingFound();
            }
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

}
