using System;
using System.Collections.Generic;

namespace CyberSecurityBott.Classes
{
    public class ChatbotEngine
    {
        private Dictionary<string, List<string>> keywordResponses;
        private Random random = new Random();

        public string CurrentTopic { get; set; }

        public MemoryManager Memory { get; set; }

        public ChatbotEngine()
        {
            Memory = new MemoryManager();

            keywordResponses = new Dictionary<string, List<string>>()
            {
                {
                    "password",
                    new List<string>()
                    {
                        "Use strong unique passwords for every account.",
                        "Avoid using personal details in passwords.",
                        "Use a password manager to store passwords safely."
                    }
                },

                {
                    "phishing",
                    new List<string>()
                    {
                        "Never click suspicious email links.",
                        "Scammers often create fake urgency.",
                        "Always verify the sender before opening attachments."
                    }
                },

                {
                    "privacy",
                    new List<string>()
                    {
                        "Review your privacy settings regularly.",
                        "Avoid sharing sensitive information publicly.",
                        "Enable two-factor authentication for extra security."
                    }
                },

                {
                    "scam",
                    new List<string>()
                    {
                        "Be cautious of deals that seem too good to be true.",
                        "Never send money to unknown people online.",
                        "Scammers often pretend to be trusted companies."
                    }
                }
            };
        }

        public string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("my name is"))
            {
                string name = input.Replace("my name is", "").Trim();

                Memory.UserMemory["name"] = name;

                return $"Nice to meet you, {name}!";
            }

            if (input.Contains("i like"))
            {
                if (input.Contains("privacy"))
                {
                    Memory.UserMemory["topic"] = "privacy";

                    return "Great! I'll remember that you're interested in privacy.";
                }
            }

            if (input.Contains("more") || input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(CurrentTopic))
                {
                    return GetRandomResponse(CurrentTopic);
                }
            }

            foreach (var keyword in keywordResponses)
            {
                if (input.Contains(keyword.Key))
                {
                    CurrentTopic = keyword.Key;

                    return GetRandomResponse(keyword.Key);
                }
            }

            return "I'm not sure I understand. Can you try rephrasing?";
        }

        private string GetRandomResponse(string keyword)
        {
            List<string> responses = keywordResponses[keyword];

            int index = random.Next(responses.Count);

            return responses[index];
        }
    }
}