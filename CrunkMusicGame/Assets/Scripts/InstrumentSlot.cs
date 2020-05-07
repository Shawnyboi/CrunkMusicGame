using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentSlot : MonoBehaviour
{
    public GameObject instrumentObj;

    private Instrument instrument;


    private void Awake()
    {
        instrument = Instantiate(instrumentObj, this.transform).GetComponent<Instrument>();
    }
    public void playInstrument()
    {
        if (instrument != null)
        {
            instrument.play();
        }
    }
}
