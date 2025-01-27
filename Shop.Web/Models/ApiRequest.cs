using static Shop.Web.SD;
namespace Shop.Web.Models;

public class ApiRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string ApiUrl { get; set;}
    public object ApiData { get; set;}
    public string AccessToken { get; set;}
}
