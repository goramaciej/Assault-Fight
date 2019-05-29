using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update

    [Header("Screen movement")]
    [Tooltip("In m")] [SerializeField] float xRange = 16f;
    [Tooltip("In m")] [SerializeField] float yRange = 8f;


    [Tooltip("In meters^-1")] [SerializeField] float xSpeed = 35f;
    [Tooltip("In meters^-1")] [SerializeField] float ySpeed = 20f;

    [Header("Rotation")]
    [SerializeField] float rotateXFactor = 9f;
    [SerializeField] float rotateYFactor = -5f;
    [Tooltip ("Rotation response on player Y input, which causes rotation in X axis")]
            [SerializeField] float controlYFactor = -20f;

    [Tooltip("Rotation response on player X input, which causes rotation in Z axis")]
            [SerializeField] float controlZFactor = -20f;

    [SerializeField] float zRotation = 0;

    [SerializeField] Vector3 currentPos;
    [SerializeField] Vector3 currentWorldPos;

    private Vector3 startPosition;
    private Rigidbody rigidBody;
    private float xThrow, yThrow;
    private bool controlEnabled = true;

    protected Joystick joystick;
    private PlayerCollision playerCollision;

    /// <summary>
    /// PRIVATE
    /// </summary>
    private void Start() {
        startPosition = transform.localPosition;

        joystick = FindObjectOfType<Joystick>();
        playerCollision = GetComponent<PlayerCollision>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (controlEnabled) {
            GetInputAxes();
            TranslateSpaceship();
            RotateSpaceship();

            //TestPosition();
        }
    }



    /*private void TestPosition() {
        currentPos = transform.localPosition;
        currentWorldPos = transform.position;

        if (Input.GetKeyUp(KeyCode.T)) {
            SendMessage("TriggerMessage");
        }
    }*/

    private void GetInputAxes() {
        //xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        //yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        if (joystick) {
            xThrow = joystick.Horizontal;
            yThrow = joystick.Vertical;
        } else {
            //Debug.Log("No Joystick move on: " + CrossPlatformInputManager.GetAxis("Vertical"));
            xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        }

    }

    /// <summary>
    /// Translation
    /// </summary>
    private void TranslateSpaceship() {
        // Ask collider if there is possibility to go further in X axis

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float tX = transform.localPosition.x;
        float rawXpos = Mathf.Clamp(tX + xOffset, -xRange, xRange);
        if(playerCollision.collisionPosition > 0) {
            rawXpos = tX - 0.2f;
        }
        if (playerCollision.collisionPosition < 0) {
            rawXpos = tX + 0.2f;
        }


        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYpos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        float rawZpos = controlZFactor * xThrow;

        //transform.localPosition = new Vector3(rawXpos, rawYpos, transform.localPosition.z);
        transform.localPosition = new Vector3(rawXpos, rawYpos, startPosition.z);
    }

    /// <summary>
    /// Rotation
    /// </summary>
    private void RotateSpaceship() {
        Vector3 tf = transform.localPosition;

        // we're trying to drop the nose when user moves starship down, and lift it up when user moves starship up
        float playerResponseY = (Mathf.Abs(tf.y) < yRange) ? controlYFactor * yThrow : 0;
        float rotateX = (tf.y / yRange) * (-rotateXFactor) + playerResponseY; // remember the angle x depends on the position of y

        float rotateY = (tf.x / xRange) * (-rotateYFactor);

        // don't let rotate when collides on this side
        float tempXthrow = xThrow;
        if ((playerCollision.collisionPosition > 0) && (xThrow > 0)) {
            tempXthrow = 0;
        }
        if ((playerCollision.collisionPosition < 0) && (xThrow < 0)) {
            tempXthrow = 0;
        }
        float rotateZ = (Mathf.Abs(tf.x) < xRange) ? controlZFactor * tempXthrow : 0;

        /*used in course: 
         * pitch: rotation on x axis
         * yaw:  rotation on y axis
         * roll: rotation on z axis;
         */

        transform.localRotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
    }


    /// <summary>
    /// Death freezing control - received from player collision
    /// </summary>
    private void OnPlayerDeath() {          
        controlEnabled = false;
        rigidBody.useGravity = true;
    }
}
