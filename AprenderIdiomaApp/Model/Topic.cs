namespace AprenderIdiomaApp.Model
{
    internal class Topic
    {
        string description;
        string url;
        string response;

        public Topic(string description, string url, string response)
        {
            this.description = description;
            this.url = url;
            this.response = response;
        }

        public string getResponse()
        {
            return this.response;
        }
    }
}