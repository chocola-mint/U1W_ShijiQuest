using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


namespace ShijiQuest
{
    public class MoveEventSystem : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput moveDirectionInput;

        [DoNotSerialize]
        public ValueInput eventSystemInput;
        // From: https://forum.unity.com/threads/manually-trigger-navigation-events-in-script.874213/ 
        public static void Move(MoveDirection direction, EventSystem eventSystem)
        {
            AxisEventData data = new AxisEventData(eventSystem);
    
            data.moveDir = direction;
    
            data.selectedObject = eventSystem.currentSelectedGameObject;
    
            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
        }
        protected override void Definition()
        {
            //The lambda to execute our node action when the inputTrigger port is triggered.
            inputTrigger = ControlInput("", (flow) =>
            {
                Move(
                    flow.GetValue<MoveDirection>(moveDirectionInput), 
                    flow.GetValue<EventSystem>(eventSystemInput)
                );
                return outputTrigger;
            });
            outputTrigger = ControlOutput("");
            moveDirectionInput = ValueInput<MoveDirection>("moveDirection", MoveDirection.Up);
            eventSystemInput = ValueInput<EventSystem>("eventSystem", EventSystem.current);
            Requirement(eventSystemInput, inputTrigger);
            Requirement(moveDirectionInput, inputTrigger); //Specifies that we need the moveDirection value to be set before the node can run.
            Succession(inputTrigger, outputTrigger); //Specifies that the input trigger port's input exits at the output trigger port. Not setting your succession also dims connected nodes, but the execution still completes.
        }
    }
}
