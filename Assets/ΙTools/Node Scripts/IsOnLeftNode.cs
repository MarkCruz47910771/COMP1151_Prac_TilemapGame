using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsOnLeftNode : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput vector1Value;

    [DoNotSerialize]
    public ValueInput vector2Value;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private bool output;

    protected override void Definition()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
            Vector2 a = flow.GetValue<Vector2>(vector1Value);
            Vector2 b = flow.GetValue<Vector2>(vector2Value);

            output = a.x * b.y < a.y * b.x;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        vector1Value  = ValueInput<Vector2>("v1", Vector2.zero);
        vector2Value  = ValueInput<Vector2>("v2", Vector2.zero);
        resultValue = ValueOutput<bool>("result", (flow) => output);

        Requirement(vector1Value, inputTrigger);
        Requirement(vector2Value, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}
