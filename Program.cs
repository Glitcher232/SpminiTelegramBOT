using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;


namespace SPminiBot
{
    class Program{
        private static TelegramBotClient client;

        private static int counttest; 

        public static Dictionary<string,int> UsersIDMessageCount = new Dictionary<string, int>();

        public static List<string> Messages = new List<string>();

        static void Main(){
            client = new TelegramBotClient(Token.token){Timeout = TimeSpan.FromSeconds(10)};
            client.StartReceiving();
            client.OnMessage += Bot_message;

            Console.ReadKey();
        }

        private static async void Bot_message(object sender, MessageEventArgs e){
            var msg = e.Message;

            Random rnd = new Random();
            
            if(msg == null)
                return;

            Messages.Add(msg.Text);

            if (msg.Text != null){
                if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                    UsersIDMessageCount.Add(msg.From.Username,1);
            }

            if (msg.Text == "/start"){
                await client.SendTextMessageAsync(
                    replyMarkup: Panels.MainButtons(),
                    chatId: msg.Chat.Id,
                    text: "Здраствуйте, это высоко-технологический бот телеграм канала СПм\n" +
                          "Снизу есть несколько кнопок для получение информации\n" +
                          "Нажмите их и получите всёю, что вам нужно!");
            }
            

            switch (msg.Text){
                case "Реклама":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if(UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: "Заполните заявку на рекламы, в одном предложение! : \n 1. Ваш ник \n 2. Что за реклама \n 3. Сколько вы за неё готовы отдать, минимум 32 ар");

                        if (msg.Text == "Реклама"){
                            while (msg.Text == "Реклама"){
                                counttest++;
                                if(Messages.Last() != "Реклама")
                                    break;
                            }
                        }

                        await client.SendTextMessageAsync(
                            chatId: 1548313628,
                            text: $"Новая заявку на рекламу:\n{Messages.Last()}");

                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Предложение отправленно. Ожидайте ответа. ");

                    }
                    break;
                case "Предложить новость":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if(UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: "Заполните заявку на новость, в одном предложение! : \n 1. Ваш ник \n 2. Что за новость \n 3. Когда она произошла/будет проходить");

                        if (msg.Text == "Предложить новость"){
                            while (msg.Text == "Предложить новость"){
                                counttest++;
                                if(Messages.Last() != "Предложить новость")
                                    break;
                            }
                        }

                        await client.SendTextMessageAsync(
                            chatId: 1548313628,
                            text: $"Новая заявку на новость:\n{Messages.Last()}");

                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Предложение отправленно. Ожидайте ответа. ");

                    }
                    break;
                case "Создатели Телеграм канала":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if (UsersIDMessageCount[msg.From.Username] <= 10){

                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Создали телеграм канал Лига Ачивок & СПмТВ \n" +
                                  "Создал телеграм бота Glither");
                    }

                    break;
            }
            
            switch (msg.Text){
                case "Ссылки":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    
                    if (UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: Panels.LinksButtons(),
                            chatId: msg.Chat.Id,
                            text: "Выберите ссылку");
                    }

                    break;
                case "Информация сервера":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    
                    if (UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "СПм - лучший сервер");
                    }

                    break;
                
                case "Мемы":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if (UsersIDMessageCount[msg.From.Username] <= 30){
                        await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: Memes.image[rnd.Next(0, Memes.image.Length)]);
                    }

                    break;
                
                case "Прочее":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    await client.SendTextMessageAsync(
                        replyMarkup: Panels.OtherButtons(),
                        chatId: msg.Chat.Id,
                        text: "Выберите кнопку");
                    break;
            }

            switch (msg.Text){
                case "Назад":
                    UsersIDMessageCount[msg.From.Username] = 0;
                    
                    await client.SendTextMessageAsync(
                        replyMarkup: Panels.MainButtons(),
                        chatId: msg.Chat.Id,
                        text: "Выберите кнопку");
                    break;
                
                case "ВК":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if (UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "ВК",
                                    "https://vk.com/spmini")),
                            chatId: msg.Chat.Id,
                            text: "Нажмите на кнопку");
                    }

                    break;
                
                case "СПрадио":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if (UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "СПрадио",
                                    "https://spworlds.ru/radio")),
                            chatId: msg.Chat.Id,
                            text: "Нажмите на кнопку");
                    }

                    break;
                
                case "Сайт":
                    if(!UsersIDMessageCount.ContainsKey(msg.From.Username))
                        UsersIDMessageCount.Add(msg.From.Username,0);
                    UsersIDMessageCount[msg.From.Username] += 1;
                    if (UsersIDMessageCount[msg.From.Username] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "Сайт",
                                    "https://spworlds.ru/spm")),
                            chatId: msg.Chat.Id,
                            text: "Нажмите на кнопку");
                    }

                    break;
                
                case "Ссылки создателей":
                    await client.SendTextMessageAsync(
                        replyMarkup: new InlineKeyboardMarkup(
                            InlineKeyboardButton.WithUrl(
                                text: "Клик",
                                url: "https://discord.gg/vX8NVtP573")),
                        chatId: msg.Chat.Id,
                        text: "Дискорд Лиги Ачивок");
                    
                    await client.SendTextMessageAsync(
                        replyMarkup: new InlineKeyboardMarkup(
                            InlineKeyboardButton.WithUrl(
                                text: "Клик",
                                url: "https://discord.gg/bQg42s5jrH ")),
                        chatId: msg.Chat.Id,
                        text: "Дискорд СПмТВ");
                    break;
            }
            Console.WriteLine($"Text: \"{msg.Text}\" User: \"{msg.From.Username}\" Count: \"{UsersIDMessageCount[msg.From.Username]}\" ChatID: {msg.Chat.Id}");
        }
    }
}