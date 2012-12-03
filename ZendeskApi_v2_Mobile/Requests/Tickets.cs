using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ZendeskApi_v2.Extensions;
using ZendeskApi_v2.Models.Shared;
using ZendeskApi_v2.Models.Tickets;
using ZendeskApi_v2.Models.Tickets.Suspended;
using ZendeskApi_v2.Models.Users;

namespace ZendeskApi_v2.Requests
{
    public class Tickets : Core
    {
        private const string _tickets = "tickets";


        public Tickets(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public async Task<GroupTicketResponse> GetAllTicketsAsync()
        {
            return await GenericGetAsync<GroupTicketResponse>(_tickets + ".json");
        }

        public async Task<IndividualTicketResponse> GetTicketAsync(long id)
        {
            return await GenericGetAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, id));
        }
        
        public async Task<IndividualTicketResponse> CreateTicketAsync(Ticket ticket)
        {
            var body = new { ticket };
            return await GenericPostAsync<IndividualTicketResponse>(_tickets + ".json", body);
        }

        /// <summary>
        /// UpdateTicket a ticket or add comments to it. Keep in mind that somethings like the description can't be updated.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<IndividualTicketResponse> UpdateTicketAsync(Ticket ticket, Comment comment = null)
        {
            if (comment != null)
                ticket.Comment = comment;
            var body = new { ticket };

            return await GenericPutAsync<IndividualTicketResponse>(string.Format("{0}/{1}.json", _tickets, ticket.Id), body);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await GenericDeleteAsync(string.Format("{0}/{1}.json", _tickets, id));
        }
    }
}
