using Application.Events.Data;
using MassTransit;
namespace Application.Events.Flow;

public class GeneralEventStateMachineFlow : MassTransitStateMachine<EventStage> {

    public GeneralEventStateMachineFlow() {
        InstanceState(x => x.CurrentState);
        Event(() => OnStart,
            x => x.CorrelateById(m => m.Message.EventId)
                .SelectId(m => m.Message.EventId));
        Event(() => OnSubmit,
            x => x.CorrelateById(m => m.Message.EventId)
                .SelectId(m => m.Message.EventId));
        Event(() => OnSave,
            x => x.CorrelateById(m => m.Message.EventId)
                .SelectId(m => m.Message.EventId));
        Initially(
            When(OnStart)
                .TransitionTo(Initial),
            When(OnSubmit)
                .TransitionTo(Submitted),
            When(OnSave)
                .TransitionTo(Save)
        );
    }
    public State Initial { get; private set; } = null!;
    public State Submitted { get; private set; } = null!;
    public State Save { get; private set; } = null!;
    public Event<GeneralData> OnStart { get; private set; } = null!;
    public Event<GeneralData> OnSubmit { get; private set; } = null!;
    public Event<GeneralData> OnSave { get; private set; } = null!;
}