using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application
{
    public class GetByIdEventUseCase
    {
        public ResponseEventJson Execute(Guid id)
        {
            var dbContext = new PassInDbContext();

            var entity = dbContext.Events.Find(id);
            if (entity is null)
            {
                throw new PassInException("An event with this is do not exist.");
            }

            return new ResponseEventJson
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                MaximumAttendees = entity.Maximu_Attendess,
                AttendeesAmount = -1,
            };
        }
    }
}
