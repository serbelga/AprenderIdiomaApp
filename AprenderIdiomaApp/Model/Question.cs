using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprenderIdiomaApp.Model
{
    class Question
    {
        private Topic[] topics;

        public Question(Topic[] topics)
        {
            this.topics = topics;
        }

        public Topic[] getTopics()
        {
            return this.topics;
        }
    }
}
