using Events.SendEmailEvents;
using Events.TicketEvents;
using MassTransit;
using SagaStateMachine.Events;
using SagaStateMachine.Models;

namespace SagaStateMachine
{
    public class TicketStateMachine : MassTransitStateMachine<TicketStateData>
    {
        //4 states are going to be happen
        public State AddTicket { get; private set; }
        public State CancelTicket { get; private set; }
        public State CancelSendEmail { get; private set; }
        public State SendEmail { get; private set; }

        //4 events are going to happen
        public Event<IAddTicketEvent> AddTicketEvent { get; private set; }
        public Event<ICancelGenerateTicketEvent> CancelGenerateTicketEvent { get; private set; }
        public Event<ICancelSendEmailEvent> CancelSendEmailEvent { get; private set; }
        public Event<ISendEmailEvent> SendEmailEvent { get; private set; }

        /// <summary>
        /// As indicated in the ‘Initially’ section, when ‘AddTicket,’ which is an ‘IAddTicketEvent,’ occurs,
        /// it is transformed into the ‘GeneratedTicketEvent’
        /// </summary>
        public TicketStateMachine()
        {
            InstanceState(s => s.CurrentState);
            Event(() => AddTicketEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => CancelGenerateTicketEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => CancelSendEmailEvent, a => a.CorrelateById(m => m.Message.TicketId));
            Event(() => SendEmailEvent, a => a.CorrelateById(m => m.Message.TicketId));

            //A message comming from ticket service


            Initially(
                When(AddTicketEvent).Then(context =>
                {
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Message.Location;
                    context.Saga.Email = context.Message.Email;
                })
                .TransitionTo(AddTicket) // The ‘Transition’ method represents the current state of a request, and this value will be stored in the database table.
                .Publish(context => new GenerateTicketEvent(context.Saga))); //Now, the message that comes from ‘IAddTicketEvent’ is transformed into ‘IGenerateTicketEvent.’

            //During AddTicketEvent some other events might occured
            //Furthermore, in the ‘During’ section, two events could be published. The ‘Publish’ method has not been used in the ‘During’ section. However, if the ‘Publish’ method is to be used,
            //a class like ‘GenerateTicketEvent’ should be created to
            //receive the incoming messages and pass them to another event.
            During(
                AddTicket,
                When(SendEmailEvent)
                .Then(context =>
                {
                    //These values could be diffrenet
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Message.Location;

                })
                .TransitionTo(SendEmail));

            During(AddTicket,
                When(CancelGenerateTicketEvent)
                .Then(context =>
                {
                    //values can be different
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Message.Location;
                })
                .TransitionTo(CancelTicket));
            //During SendEmailEvent some other events might occured

            During(
                SendEmail,
                When(CancelSendEmailEvent)
                .Then(context =>
                {
                    // These values could be different 
                    context.Saga.TicketId = context.Message.TicketId;
                    context.Saga.Title = context.Message.Title;
                    context.Saga.Email = context.Message.Email;
                    context.Saga.TicketNumber = context.Message.TicketNumber;
                    context.Saga.Age = context.Message.Age;
                    context.Saga.Location = context.Message.Location;
                }).TransitionTo(CancelSendEmail)
                );
        }
    }
}
