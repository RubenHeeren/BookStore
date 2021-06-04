namespace BookStore.UI.Static
{
    public static class Endpoints
    {

#if DEBUG
        public static string BaseUrl = "https://localhost:44376";
#else
        public static string BaseUrl = "https://edubookstoreapi.azurewebsites.net/";
#endif

        public static string AuthorsEndpoint = $"{BaseUrl}/api/authors/";
        public static string BooksEndpoint = $"{BaseUrl}/api/books/";
        public static string RegisterEndpoint = $"{BaseUrl}/api/users/register/";
        public static string LoginEndpoint = $"{BaseUrl}/api/users/login/";
    }
}
