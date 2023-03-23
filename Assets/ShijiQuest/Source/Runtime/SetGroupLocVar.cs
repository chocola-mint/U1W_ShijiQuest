using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace ShijiQuest
{
    public class SetGroupLocVar : Unit
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput inputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput outputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ValueInput variableGroupInput;
        [DoNotSerialize]
        public ValueInput keyInput;
        [DoNotSerialize]
        public ValueInput valueInput;
        protected override void Definition()
        {
            //The lambda to execute our node action when the inputTrigger port is triggered.
            inputTrigger = ControlInput("", (flow) =>
            {
                var varGroup = flow.GetValue<VariablesGroupAsset>(variableGroupInput);
                var key = flow.GetValue<string>(keyInput);
                var value = flow.GetValue<IVariable>(valueInput);
                if(!varGroup.TryAdd(key, value))
                {
                    varGroup.Remove(key);
                    varGroup.Add(key, value);
                }
                return outputTrigger;
            });
            outputTrigger = ControlOutput("");
            variableGroupInput = ValueInput<VariablesGroupAsset>("varGroup");
            keyInput = ValueInput<string>("key");
            valueInput = ValueInput<IVariable>("value");
            Requirement(variableGroupInput, inputTrigger);
            Requirement(keyInput, inputTrigger);
            Requirement(valueInput, inputTrigger);
            Succession(inputTrigger, outputTrigger); //Specifies that the input trigger port's input exits at the output trigger port. Not setting your succession also dims connected nodes, but the execution still completes.
        }
    }
}
