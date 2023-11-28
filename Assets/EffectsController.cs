using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class EffectsController : MonoBehaviour
{

    //remember to drag and drop your scriptable object into this field in the inspector...
    private Vignette vignette = null;
    private ChromaticAberration chromaticAberration = null;
    private LensDistortion lensDistortion = null;
    private Volume volumeFast;
    private Volume volumeSlow;
    
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setProfiles(GameObject slow, GameObject fast)
    {
        volumeSlow = slow.GetComponent<Volume>();
        volumeFast = fast.GetComponent<Volume>();

        // volumeFast.sharedProfile.TryGet<Vignette>(out vignette);
        // volumeSlow.sharedProfile.TryGet<ChromaticAberration>(out chromaticAberration);
        // volumeSlow.sharedProfile.TryGet<LensDistortion>(out lensDistortion);
    }

    void Update()
    {
        // if (chromaticAberration != null)
        // {
        //     chromaticAberration.intensity.SetValue(new NoInterpMinFloatParameter(1f, 0, true));
        // }
    }
}
