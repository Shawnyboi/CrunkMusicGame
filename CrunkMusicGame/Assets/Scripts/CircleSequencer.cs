using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSequencer : MonoBehaviour
{
    public int bpm;
    public int bars;
    public int slotsPerBar;
    public float radius;

    public GameObject instrumentSlotPrefab;
    public GameObject needle;

    private bool playing;
    private float currentAngle;
    private List<InstrumentSlot> instrumentSlots;

    private void initialize()
    {
        playing = true;
        currentAngle = 0f;
        instrumentSlots = new List<InstrumentSlot>();
        initializeSlots();
    }

    private void initializeSlots()
    {
        float angleBetweenSlots = 360f / (bars * slotsPerBar);
        for(int i = 0; i < bars * slotsPerBar; i++)
        {
            float currentAngle = i * angleBetweenSlots * Mathf.PI / 180f;
            Vector3 instancePoint = this.transform.position + (radius * (this.transform.rotation * (new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)))));
            GameObject inst = Instantiate(instrumentSlotPrefab, instancePoint, Quaternion.identity, this.transform);
            InstrumentSlot slot = inst.GetComponent<InstrumentSlot>();
            instrumentSlots.Add(slot);
        }
    }

    void Awake()
    {
        initialize();
    }

    private void Start()
    {
        StartCoroutine(play());
    }

    private int passedOverInstrumentSlot(float previousAngle)
    {
        if(previousAngle > currentAngle)
        {
            return 0;
        }
        else
        {
            float angleBetweenSlots = 360f / (bars * slotsPerBar);
            int nextSlot = Mathf.CeilToInt(previousAngle / angleBetweenSlots);
            if(currentAngle > nextSlot * angleBetweenSlots)
            {
                return nextSlot;
            }
            else
            {
                return -1;
            }
        }
    }
    private void playInstrumentIfPassed(float previousAngle)
    {
        int slotPassed = passedOverInstrumentSlot(previousAngle);
        if (slotPassed != -1)
        {
            instrumentSlots[slotPassed].playInstrument();
        }
    }

    private void moveNeedle()
    {
        needle.transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
    private void playingUpdate(float previousAngle)
    {
        playInstrumentIfPassed(previousAngle);
        moveNeedle();
    }
    private IEnumerator play()
    {
        float timePassed = 0f;
        float timeToRotate = 4f * bars / (bpm / 60f);
        float previousAngle = currentAngle;
        while (playing)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeToRotate)
            {
                timePassed -= timeToRotate;
            }
            previousAngle = currentAngle;
            currentAngle = (timePassed / timeToRotate) * 360f;
            playingUpdate(previousAngle);
            yield return null;
        }
        yield return null;
    }
}
