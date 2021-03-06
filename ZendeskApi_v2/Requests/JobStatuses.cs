using ZendeskApi_v2.Models.Shared;

namespace ZendeskApi_v2.Requests
{
    public class JobStatuses : Core
    {

        public JobStatuses(string yourZendeskUrl, string user, string password)
            : base(yourZendeskUrl, user, password)
        {
        }

        public JobStatusResponse GetJobStatus(string id)
        {
            return GenericGet<JobStatusResponse>(string.Format("job_statuses/{0}.json", id));
        }

    }
}