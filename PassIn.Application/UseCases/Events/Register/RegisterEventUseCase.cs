using PassIn.Communication;
using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application
{
    public class RegisterEventUseCase
    {
        public ResponseRegisterEventsJson Execute(RequestEventJson request)
        {
            this.Validate(request);

            var eventEntity = new Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximu_Attendess = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-"),
            };

            var dbContext = new PassInDbContext();
            dbContext.Events.Add(eventEntity);
            dbContext.SaveChanges();

            return new ResponseRegisterEventsJson
            {
                Id = eventEntity.Id
            };
        }

        private void Validate(RequestEventJson request)
        {
            if (request.MaximumAttendees <= 0)
            {
                throw new PassInException("The Maxium attendees is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new PassInException("The Title is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ArgumentException("The Details is invalid.");
            }
        }
    }
}
