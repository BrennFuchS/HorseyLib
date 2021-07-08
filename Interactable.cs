using UnityEngine;

/// <summary>A class to make interaction easier</summary>
public abstract class Interactable : MonoBehaviour
{
    public bool mouseIsOver;
    public bool mouseEntered;
    public bool lClicked;
    public bool rClicked;

    /// <summary>Called when the player's cursor first moves over the object</summary>
    public virtual void mouseEnter() { }

    /// <summary>Called when the player's cursor is over the object</summary>
    public virtual void mouseOver() { }

    /// <summary>Called when the player's cursor is exits object</summary>
    public virtual void mouseExit() { }

    /// <summary>Called when the player scrolls up and moused over</summary>
    public virtual void scrollUp() { }

    /// <summary>Called when the player scrolls down and moused over</summary>
    public virtual void scrollDown() { }

    /// <summary>Called when the player presses the use key and moused over</summary>
    public virtual void use() { }

    /// <summary>Called when the player presses the left mouse button and moused over</summary>
    public virtual void lClick() { }

    /// <summary>Called when the player holds the left mouse button and moused over</summary>
    public virtual void lHold() { }

    /// <summary>Called when the player releases the left mouse button or mouses off</summary>
    public virtual void lRelease() { }

    /// <summary>Called when the player presses the right mouse button and moused over</summary>
    public virtual void rClick() { }

    /// <summary>Called when the player holds the right mouse button and moused over</summary>
    public virtual void rHold() { }

    /// <summary>Called when the player releases the right mouse button or mouses off</summary>
    public virtual void rRelease() { }
}

class InteractableHandler : MonoBehaviour
{
    internal Camera cam;
    Interactable obj;
    RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1))
        {
            obj = hit.collider.GetComponent<Interactable>();
            if (obj)
            {
                obj.mouseIsOver = true;
                if (!obj.mouseEntered)
                {
                    obj.mouseEntered = true;
                    obj.mouseEnter();
                }
                obj.mouseOver();
                if (cInput.GetKeyDown("User")) obj.use();
                if (Input.GetMouseButtonDown(0))
                {
                    obj.lClick();
                    obj.lClicked = true;
                }
                if (Input.GetMouseButton(0)) obj.lHold();
                if (Input.GetMouseButtonUp(0))
                {
                    obj.lClick();
                    obj.lClicked = false;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    obj.rClick();
                    obj.rClicked = true;
                }
                if (Input.GetMouseButton(1)) obj.rHold();
                if (Input.GetMouseButtonUp(1))
                {
                    obj.rClick();
                    obj.rClicked = false;
                }
                if (Input.mouseScrollDelta.y > 0) obj.scrollUp();
                if (Input.mouseScrollDelta.y < 0) obj.scrollDown();
            }
        }
        else if (obj)
        {
            obj.mouseExit();
            obj.mouseEntered = false;
            obj.lClicked = false;
            obj.rClicked = false;
            if (obj.lClicked) obj.lRelease();
            if (obj.rClicked) obj.rRelease();
            obj = null;
        }
    }
}