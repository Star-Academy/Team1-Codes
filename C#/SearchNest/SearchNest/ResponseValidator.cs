using Nest;

namespace SearchNest
{
    public static class ResponseValidator
    {
        public static bool IsResponseValid(IResponse searchResponse) {
            return searchResponse.IsValid; // TODO ._.
        }
    }
}