namespace AprenderIdiomaApp.Model
{
    internal class Topic
    {
        string description;
        string url;
        string answer;

        public Topic(string description, string url, string response)
        {
            this.description = description;
            this.url = url;
            this.answer = response;
        }

        public string getAnswer()
        {
            return this.answer;
        }

        public string getUrl()
        {
            return this.url;
        }
    }
}