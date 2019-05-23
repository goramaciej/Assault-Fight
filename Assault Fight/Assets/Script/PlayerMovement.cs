using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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


    private float xThrow, yThrow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        GetInputAxes();
        TranslateSpaceship();
        RotateSpaceship();
    }
    private void GetInputAxes() {
        // AXES is the plural of AXIS :D
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
    }

    private void TranslateSpaceship() {
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXpos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);

        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYpos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        float rawZpos = controlZFactor * xThrow;

        transform.localPosition = new Vector3(rawXpos, rawYpos, transform.localPosition.z);
    }
    private void RotateSpaceship() {
        Vector3 tf = transform.localPosition;

        // we're trying to drop the nose when user moves starship down, and lift it up when user moves starship up
        float playerResponseY = (Mathf.Abs(tf.y) < yRange) ? controlYFactor * yThrow : 0;
        float rotateX = (tf.y / yRange) * (-rotateXFactor) + playerResponseY; // remember the angle x depends on the position of y

        float rotateY = (tf.x / xRange) * (-rotateYFactor);

        float rotateZ = (Mathf.Abs(tf.x) < xRange) ? controlZFactor * xThrow : 0;

        /*used in course: 
         * pitch: rotation on x axis
         * yaw:  rotation on y axis
         * roll: rotation on z axis;
         */

        transform.localRotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
    }
}
