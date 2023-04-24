using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularParticleChooser : ParticleChooserBase
{
    [SerializeField] private GameObject MassSliderObject;
    [SerializeField] private GameObject ChargeSliderObject;

    private InputField MassValueText;
    private InputField ChargeValueText;

    private float MaxChargeValue;

    private float Mass;
    private float Charge;
    private bool IsStatic = false;

    Slider massSlider;
    Slider ChargeSlider;


    private void Start()
    {
        MassValueText = MassSliderObject.GetComponentInChildren<InputField>();
        ChargeValueText = ChargeSliderObject.GetComponentInChildren<InputField>();

        massSlider = MassSliderObject.GetComponent<Slider>(); //Mass slider
        SetMass((massSlider.maxValue + massSlider.minValue) / 2);
        massSlider.value = Mass;

        ChargeSlider = ChargeSliderObject.GetComponent<Slider>(); //Chargeslider
        SetCharge((ChargeSlider.maxValue + ChargeSlider.minValue) / 2);
        ChargeSlider.value = Charge;
        MaxChargeValue = ChargeSlider.maxValue;
    }


    public void SetMass(float value)
    {
        Mass = value;
        MassValueText.text = Mass.ToString();

    }

    public void SetMass(string value)
    {
        float parsedValue = float.Parse(value);

        massSlider.value = parsedValue;
        SetMass(parsedValue);
    }

    public void SetCharge(float value)
    {
        Charge = value;
        ChargeValueText.text = Charge.ToString();
    }

    public void SetCharge(string value)
    {
        float parsedValue = float.Parse(value);
        
        ChargeSlider.value = parsedValue;
        SetCharge(parsedValue);
    }

    public void SetStatic(bool value)
    {
        IsStatic = value;
    }

    public override GameObject CreateParticle(Vector2 position)
    {
        GameObject particle = Instantiate(particleObject, position, Quaternion.identity);
        ParticleController particleController = particle.GetComponent<ParticleController>();

        particleController.SetMass(this.Mass);
        particleController.Charge = this.Charge;
        particleController.SetStatic(this.IsStatic);

        particleController.chargeController = particle.AddComponent<RegularChargeBehaviour>();
        particleController.SetInteractionController(particle.AddComponent<RegularInteractionBehaviour>());

        Color particleColor;

        if (Charge > 0)
        {
            particleColor = Color.HSVToRGB(0f, Mathf.Abs(Charge) / MaxChargeValue * 0.9f + 0.1f, 1);
        } else if (Charge < 0)
        {
            particleColor = Color.HSVToRGB(0.65f, Mathf.Abs(Charge) / MaxChargeValue * 0.9f + 0.1f, 1);
        } else
        {
            particleColor = Color.HSVToRGB(0f, 0f, 0.5f);
        }

        particle.GetComponent<SpriteRenderer>().color = particleColor;

        return particle;
    }
}
