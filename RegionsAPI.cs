using RegionsAPI.Data;
using RestSharp;
using RestSharp.Serializers.Utf8Json;

namespace RegionsAPI.User
{
    public class RegionsAPIUser
    {
        const string BaseUrl = "https://regions-test.2gis.com/1.0/regions";
        readonly IRestClient _client;
        private string _lastQuery;
        private int _page;
        public RegionsAPIUser()
        {
            _client = new RestClient(BaseUrl);
            _client.UseUtf8Json();
            _page = 0;
        }
        private T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);
            //if (response.ErrorException != null)
            //{
            //    const string message = "Error retrieving response.  Check inner details for more info.";
            //    var twilioException = new Exception(message, response.ErrorException);
            //    throw twilioException;
            //}
            return response.Data;
        }
        public Call GetCall(string Query)
        {
            var request = new RestRequest(Query);
            _lastQuery = Query;
            _page = 0;
            return Execute<Call>(request);
        }
        public Call NextPage()
        {
            var request = new RestRequest(_lastQuery + string.Format("&page={0}", ++_page));
            return Execute<Call>(request);
        }
    }
}
