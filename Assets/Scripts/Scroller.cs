using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    [SerializeField] private float scrollDuration = 5f;  
    [SerializeField] private float transitionDuration = 1f;  
    
    private float timer = 0f;
    private Vector2 currentDirection;
    private Vector2 targetDirection;
    private float transitionTimer = 0f;
    private bool transitioning = false;

    void Start()
    {
        // Set initial direction
        currentDirection = new Vector2(_x, _y);
        targetDirection = currentDirection;
    }

    void Update()
    {
        // timer for changing directions (because the image being mirrored messes it up, so it can't be infinite.)
        timer += Time.deltaTime;
        if (!transitioning && timer >= scrollDuration)
        {
            transitioning = true;
            transitionTimer = 0f;
            targetDirection = -currentDirection;
        }
        if (transitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            
            // Apply ease-in-out
            float smoothT = Mathf.SmoothStep(0, 1, t);
            
            Vector2 scrollDirection = Vector2.Lerp(currentDirection, targetDirection, smoothT);

            _img.uvRect = new Rect(_img.uvRect.position + scrollDirection * Time.deltaTime, _img.uvRect.size);
            
            if (transitionTimer >= transitionDuration)
            {
                transitioning = false;
                currentDirection = targetDirection;
                timer = 0f;  // Reset main timer
            }
        }
        else
        {
            // Default scrolling
            _img.uvRect = new Rect(_img.uvRect.position + currentDirection * Time.deltaTime, _img.uvRect.size);
        }
    }
}