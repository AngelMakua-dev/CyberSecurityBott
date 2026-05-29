using CyberSecurityBot.Classes;
using System;
using System.Windows;

namespace CyberSecurityBott
{
    public partial class MainWindow : Window
    {
        private ChatbotEngine bot;
        private SentimentAnalyzer sentiment;

        // DELEGATE
        public delegate void MessageHandler(string message);

        public MainWindow()
        {
            InitializeComponent();

            bot = new ChatbotEngine();

            sentiment = new SentimentAnalyzer();

            AudioPlayer.PlayGreeting();

            AppendBotMessage("Hello! I'm your Cybersecurity Awareness Assistant.");
            AppendBotMessage("Tell me your name to get started.");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            AppendUserMessage(input);

            string mood = sentiment.DetectSentiment(input);

            if (mood == "worried")
            {
                AppendBotMessage("It's understandable to feel worried. Let me help you stay safe online.");
            }

            else if (mood == "frustrated")
            {
                AppendBotMessage("Cybersecurity can feel overwhelming sometimes, but I'm here to help.");
            }

            else if (mood == "curious")
            {
                AppendBotMessage("That's great! Learning cybersecurity is very important.");
            }

            string response = bot.GetResponse(input);

            // DELEGATE USAGE
            MessageHandler handler = AppendBotMessage;
            handler(response);

            UserInput.Clear();
        }

        private void AppendBotMessage(string message)
        {
            ChatDisplay.AppendText($"BOT: {message}\n\n");
        }

        private void AppendUserMessage(string message)
        {
            ChatDisplay.AppendText($"YOU: {message}\n\n");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ChatDisplay.Clear();
        }
    }
}