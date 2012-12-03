using System;
using System.Text;
using ZendeskApi_v2.Requests;

namespace ZendeskApi_v2
{
    public class ZendeskApi
    {
        public Tickets Tickets { get; set; }        

        public string ZendeskUrl { get; set; }        

        public ZendeskApi(string yourZendeskUrl, string user, string password)
        {
            Tickets = new Tickets(yourZendeskUrl, user, password);
            
            ZendeskUrl = yourZendeskUrl;
        }       
    }
}
