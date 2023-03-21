using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

namespace ShijiQuest
{
    public class SubmitEventSystem : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput eventSystemInput;
        // From: https://forum.unity.com/threads/manually-trigger-navigation-events-in-script.874213/ 
        public static void Submit(EventSystem eventSystem)
        {
            AxisEventData data = new AxisEventData(eventSystem);
    
            data.selectedObject = eventSystem.currentSelectedGameObject;
    
            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.submitHandler);
        }
        protected override void Definition()
        {
            //The lambda to execute our node action when the inputTrigger port is triggered.
            inputTrigger = ControlInput("", (flow) =>
            {
                Submit(flow.GetValue<EventSystem>(eventSystemInput));
                return outputTrigger;
            });
            outputTrigger = ControlOutput("");
            eventSystemInput = ValueInput<EventSystem>("eventSystem", EventSystem.current);
            Requirement(eventSystemInput, inputTrigger);
            Succession(inputTrigger, outputTrigger); //Specifies that the input trigger port's input exits at the output trigger port. Not setting your succession also dims connected nodes, but the execution still completes.
        }
    }
}
