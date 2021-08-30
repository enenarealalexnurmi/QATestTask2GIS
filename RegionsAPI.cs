using RegionsAPI.Data;
using RestSharp;
using RestSharp.Serializers.Utf8Json;
using System.Net;

namespace RegionsAPI.User
{
    public class RegionsAPIUser
    {
        const string BaseUrl = "https://regions-test.2gis.com/1.0/regions";
        readonly IRestClient _client;
        private string _lastQuery;
        private int _page;
        public string ErrorMessage { get; set; }
        public ResponseStatus RespStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public RegionsAPIUser()
        {
            _client = new RestClient(BaseUrl);
            _client.UseUtf8Json();
            _page = 1;
        }
        private T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);
            ErrorMessage = response.ErrorMessage;
            StatusCode = response.StatusCode;
            RespStatus = response.ResponseStatus;
            return response.Data;
        }
        public Call GetCall(string Query)
        {
            var request = new RestRequest(Query);
            _lastQuery = Query;
            _page = 1;
            return Execute<Call>(request);
        }
        public Call NextPage()
        {
            char typeAdd = _lastQuery == "" ? '?' : '&';
            var request = new RestRequest(_lastQuery + string.Format("{0}page={1}", typeAdd, ++_page));
            return Execute<Call>(request);
        }
    }
}
