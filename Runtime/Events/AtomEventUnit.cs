using System;
using Unity.VisualScripting;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Void = UnityAtoms.Void;

namespace Guyl.BoltAtoms.Events
{
    [IncludeInSettings(true)]
    public abstract class AtomEventUnit<T, ER, E> : MachineEventUnit<EmptyEventArgs>
        where E : AtomEvent<T>
        where ER : IGetEvent, ISetEvent, new()
    {
        public new class Data : EventUnit<EmptyEventArgs>.Data
        {
            public T CurrentValue;
            public Action<T> EventRaisedHandler;
        }

        [DoNotSerialize]
        public ValueInput _event;

        [DoNotSerialize]
        public ValueOutput _value;

        protected override string hookName => "UnityAtomEvent";

        public override IGraphElementData CreateData()
        {
            return new Data();
        }

        protected override void Definition()
        {
            base.Definition();

            _event = ValueInput<ER>("", default);
            _value = ValueOutput("", flow => flow.stack.GetElementData<Data>(this).CurrentValue);
        }

        public override void StartListening(GraphStack stack)
        {
            base.StartListening(stack);

            Data data = stack.GetElementData<Data>(this);
            Flow flow = Flow.New(stack.ToReference());
            E currentEvent = flow.GetValue<ER>(_event).GetEvent<E>();

            if (!currentEvent)
            {
                return;
            }

            data.EventRaisedHandler = value =>
            {
                data.CurrentValue = value;
                flow.Invoke(trigger);
            };

            currentEvent.Unregister(data.EventRaisedHandler);
            currentEvent.Register(data.EventRaisedHandler);
        }

        public override void StopListening(GraphStack stack)
        {
            base.StopListening(stack);

            Data data = stack.GetElementData<Data>(this);
            ER currentEventRef = Flow.FetchValue<ER>(_event, stack.ToReference());
            E currentEvent = currentEventRef.GetEvent<E>();

            if (!currentEvent)
            {
                return;
            }

            currentEvent.Unregister(data.EventRaisedHandler);
        }
    }

    [UnitCategory("Events/Atoms"), UnitShortTitle("AtomBaseVariable Event"), UnitTitle("AtomBaseVariable Event")]
    public class
        AtomBaseVariableEventUnit : AtomEventUnit<AtomBaseVariable, AtomBaseVariableBaseEventReference,
            AtomBaseVariableEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Bool Event"), UnitTitle("Atom Bool Event")]
    public class BoolEventUnit : AtomEventUnit<bool, BoolEventReference, BoolEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("BoolPair Event"), UnitTitle("Atom BoolPair Event")]
    public class BoolPairEventUnit : AtomEventUnit<BoolPair, BoolPairEventReference, BoolPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collider2D Event"), UnitTitle("Atom Collider2D Event")]
    public class Collider2DEventUnit : AtomEventUnit<Collider2D, Collider2DEventReference, Collider2DEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collider2DPair Event"), UnitTitle("Atom Collider2DPair Event")]
    public class
        Collider2DPairEventUnit : AtomEventUnit<Collider2DPair, Collider2DPairEventReference, Collider2DPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collider Event"), UnitTitle("Atom Collider Event")]
    public class ColliderEventUnit : AtomEventUnit<Collider, ColliderEventReference, ColliderEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("ColliderPair Event"), UnitTitle("Atom ColliderPair Event")]
    public class ColliderPairEventUnit : AtomEventUnit<ColliderPair, ColliderPairEventReference, ColliderPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collision2D Event"), UnitTitle("Atom Collision2D Event")]
    public class Collision2DEventUnit : AtomEventUnit<Collision2D, Collision2DEventReference, Collision2DEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collision2DPair Event"), UnitTitle("Atom Collision2DPair Event")]
    public class
        Collision2DPairEventUnit : AtomEventUnit<Collision2DPair, Collision2DPairEventReference,
            Collision2DPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Collision Event"), UnitTitle("Atom Collision Event")]
    public class CollisionEventUnit : AtomEventUnit<Collision, CollisionEventReference, CollisionEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("CollisionPair Event"), UnitTitle("Atom CollisionPair Event")]
    public class
        CollisionPairEventUnit : AtomEventUnit<CollisionPair, CollisionPairEventReference, CollisionPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Color Event"), UnitTitle("Atom Color Event")]
    public class ColorEventUnit : AtomEventUnit<Color, ColorEventReference, ColorEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("ColorPair Event"), UnitTitle("Atom ColorPair Event")]
    public class ColorPairEventUnit : AtomEventUnit<ColorPair, ColorPairEventReference, ColorPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Double Event"), UnitTitle("Atom Double Event")]
    public class DoubleEventUnit : AtomEventUnit<double, DoubleEventReference, DoubleEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("DoublePair Event"), UnitTitle("Atom DoublePair Event")]
    public class DoublePairEventUnit : AtomEventUnit<DoublePair, DoublePairEventReference, DoublePairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Float Event"), UnitTitle("Atom Float Event")]
    public class FloatEventUnit : AtomEventUnit<float, FloatEventReference, FloatEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("FloatPair Event"), UnitTitle("Atom FloatPair Event")]
    public class FloatPairEventUnit : AtomEventUnit<FloatPair, FloatPairEventReference, FloatPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("GameObject Event"), UnitTitle("Atom GameObject Event")]
    public class GameObjectEventUnit : AtomEventUnit<GameObject, GameObjectEventReference, GameObjectEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("GameObjectPair Event"), UnitTitle("Atom GameObjectPair Event")]
    public class
        GameObjectPairEventUnit : AtomEventUnit<GameObjectPair, GameObjectPairEventReference, GameObjectPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Int Event"), UnitTitle("Atom Int Event")]
    public class IntEventUnit : AtomEventUnit<int, IntEventReference, IntEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("IntPair Event"), UnitTitle("Atom IntPair Event")]
    public class IntPairEventUnit : AtomEventUnit<IntPair, IntPairEventReference, IntPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Quaternion Event"), UnitTitle("Atom Quaternion Event")]
    public class QuaternionEventUnit : AtomEventUnit<Quaternion, QuaternionEventReference, QuaternionEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("QuaternionPair Event"), UnitTitle("Atom QuaternionPair Event")]
    public class
        QuaternionPairEventUnit : AtomEventUnit<QuaternionPair, QuaternionPairEventReference, QuaternionPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("String Event"), UnitTitle("Atom String Event")]
    public class StringEventUnit : AtomEventUnit<string, StringEventReference, StringEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("StringPair Event"), UnitTitle("Atom StringPair Event")]
    public class StringPairEventUnit : AtomEventUnit<StringPair, StringPairEventReference, StringPairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Vector2 Event"), UnitTitle("Atom Vector2 Event")]
    public class Vector2EventUnit : AtomEventUnit<Vector2, Vector2EventReference, Vector2Event> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Vector2Pair Event"), UnitTitle("Atom Vector2Pair Event")]
    public class Vector2PairEventUnit : AtomEventUnit<Vector2Pair, Vector2PairEventReference, Vector2PairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Vector3 Event"), UnitTitle("Atom Vector3 Event")]
    public class Vector3EventUnit : AtomEventUnit<Vector3, Vector3EventReference, Vector3Event> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Vector3Pair Event"), UnitTitle("Atom Vector3Pair Event")]
    public class Vector3PairEventUnit : AtomEventUnit<Vector3Pair, Vector3PairEventReference, Vector3PairEvent> { }

    [UnitCategory("Events/Atoms"), UnitShortTitle("Void Event"), UnitTitle("Atom Void Event")]
    public class VoidEventUnit : AtomEventUnit<Void, VoidBaseEventReference, VoidEvent> { }
}