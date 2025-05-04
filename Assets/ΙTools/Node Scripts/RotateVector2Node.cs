using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateVector2Node : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput vectorValue;

    [DoNotSerialize]
    public ValueInput angleValue;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private Vector2 output;

    protected override void Definition()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
            Vector2 vector = flow.GetValue<Vector2>(vectorValue);
            float angle = flow.GetValue<float>(angleValue);

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector2.up);
            output = rotation * vector;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        vectorValue  = ValueInput<Vector2>("vector", Vector2.zero);
        angleValue = ValueInput<float>("angle", 0);
        resultValue = ValueOutput<Vector2>("result", (flow) => output);

        Requirement(vectorValue, inputTrigger);
        Requirement(angleValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}
