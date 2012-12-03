using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ZendeskApi_v2;
using ZendeskApi_v2.Models.Constants;
using ZendeskApi_v2.Models.Tickets;

namespace Tests
{
    [TestFixture]
    public class TicketTests
    {        
        ZendeskApi api = new ZendeskApi(Settings.Site, Settings.Email, Settings.Password);

        [Test]
        public async Task CanGetTickets()
        {
            var tickets = api.Tickets.GetAllTicketsAsync();            
            Assert.True(tickets.Result.Count > 0);
        }

        [Test]
        public void CanCreateUpdateAndDeleteTicket()
        {
            var ticket = new Ticket()
                             {
                                 Subject = "my printer is on fire",
                                 Description = "HELP",
                                 Priority = TicketPriorities.Urgent
                             };

            var res = api.Tickets.CreateTicketAsync(ticket).Result.Ticket;

            Assert.NotNull(res);
            Assert.Greater(res.Id, 0);

            res.Status = TicketStatus.Solved;
            res.AssigneeId = Settings.UserId;

            res.CollaboratorEmails = new List<string>(){ Settings.ColloboratorEmail};
            var body = "got it thanks";
            var updateResponse = api.Tickets.UpdateTicketAsync(res, new Comment() {Body = body, Public = true});

            Assert.NotNull(updateResponse.Result);
            Assert.AreEqual(updateResponse.Result.Audit.Events.First().Body, body);

            Assert.True(api.Tickets.DeleteAsync(res.Id).Result);
        }
    }
}
