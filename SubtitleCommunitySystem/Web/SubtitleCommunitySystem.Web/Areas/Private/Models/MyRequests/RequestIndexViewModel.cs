namespace SubtitleCommunitySystem.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class RequestIndexViewModel
    {
        public RequestIndexViewModel()
        {
            this.PendingRequests = new HashSet<RequestOutputModel>();
            this.ApprovedRequests = new HashSet<RequestOutputModel>();
            this.DeniedRequests = new HashSet<RequestOutputModel>();
        }

        public IEnumerable<RequestOutputModel> PendingRequests { get; set; }

        public IEnumerable<RequestOutputModel> ApprovedRequests { get; set; }

        public IEnumerable<RequestOutputModel> DeniedRequests { get; set; }
    }
}