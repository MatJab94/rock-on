using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClefColumn_PlayCode : MonoBehaviour {

    // the Door this Clef is connected with, set in Inspector for every Clef
    public ClosedDoor_Open doorScript;

    // animator of this clef
    private Animator _clefAnim;

    // the code sequence that opens the door
    private int[] _closedDoorCode;

    // script that actually shows the code on the column
    private ClefColumn_Code _codeScript;

    // Use this for initialization
    void Start () {
        _clefAnim = GetComponentInChildren<Animator>();
        _codeScript = GetComponentInChildren<ClefColumn_Code>();
    }

    public void activateClefAndDoor()
    {
        // activate the door
        doorScript.activateDoor();

        // animate the clef to show it's active
        _clefAnim.SetBool("active", true);

        // get the door's secret code
        _closedDoorCode = doorScript.getSecretCode();

        // start showing the code on the column
        _codeScript.startShowingCode(_closedDoorCode);
    }

    public bool isDoorOpen()
    {
        return doorScript.isOpen();
    }
}
