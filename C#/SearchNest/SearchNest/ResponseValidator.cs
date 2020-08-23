using Nest;

namespace SearchNest
{
    public static class ResponseValidator
    {
        public static bool IsSearchSuccessful(IResponse searchResponse) {
            return searchResponse.ApiCall.Success;
        }
    }
}