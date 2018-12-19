using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprenderIdiomaApp.Model
{
    class Question1
    {
        string answer;
        string topic;

        public Question1(string answer, string topic)
        {
            this.answer = answer;
            this.topic = topic;
        }

        public string getAnswer() => this.answer;

        public string getTopic() => this.topic;

    }
}
