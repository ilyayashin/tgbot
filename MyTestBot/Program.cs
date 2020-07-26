using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace MyTestBot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        public static string listcommands = "Все существующие команды: \n" +
            "/start - приветствие! \n" +
            "/инфа - комманды \n" +
            "/контакты - мои контакты";
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1373989007:AAGD9rxZRfvL0Y5HYyg-RCInO0bJeDnPYac") {Timeout = TimeSpan.FromSeconds(10)};
            var me = botClient.GetMeAsync().Result;

            botClient.OnMessage += Bot_OnMessage;

            botClient.StartReceiving();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                case "/start":
                    await Startmes(message);
                    break;
                case "/инфа":
                    await infomes(message);
                    break;
                case "/контакты":
                    await contactmes(message);
                    break;
                default:
                    await defmes(message);
                    break;
            }

        }

        static async Task defmes(Message message)
        {
            await botClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               text: "Я не понимаю о чем ты. Почитай это - \n" +
               $"{listcommands}"
               );
        }

        static async Task contactmes(Message message)
        {
            await botClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               text: "У меня нет телефона, я же бот :|"
               );
        }

        static async Task infomes(Message message)
        {
            await botClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               text: $"{listcommands}"
               );
        }

        static async Task Startmes(Message message)
        {
            string username = $"{message.From.FirstName} {message.From.LastName}";
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Здравствуй {username}, я - новый бот, созданный Яшиным Ильёй \n"+
                "и это моя демонстрация \n" +
                $"{listcommands}"
                );
        }
    }
}
