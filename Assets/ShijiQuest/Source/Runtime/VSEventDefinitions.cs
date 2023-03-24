using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

namespace ShijiQuest
{
    public abstract class SingleArgumentGOEventUnit<T, R> : GameObjectEventUnit<T>
    {
        [DoNotSerialize]// No need to serialize ports.
        public ValueOutput selected { get; private set; }// The Event output data to return when the Event is triggered.
        public abstract string argName { get; }
        public override Type MessageListenerType => typeof(R);
        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            selected = ValueOutput<T>(argName);
        }
        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, T data)
        {
            flow.SetValue(selected, data);
        }
    }

    [UnitTitle("On Spell Selected")]// The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Events\\ShijiQuest")] // Set the path to find the node in the fuzzy finder.
    public class EventOnSpellSelected : SingleArgumentGOEventUnit<SpellData, MagicMenu>
    {
        protected override string hookName => nameof(EventOnSpellSelected);
        public override string argName => "result";
    }
    [UnitTitle("On Item Selected")]// The Custom Scripting Event node to receive the Event. Add "On" to the node title as an Event naming convention.
    [UnitCategory("Events\\ShijiQuest")] // Set the path to find the node in the fuzzy finder.
    public class EventOnItemSelected : SingleArgumentGOEventUnit<ItemData, ItemMenu>
    {
        protected override string hookName => nameof(EventOnItemSelected);
        public override string argName => "result";
    }
}
