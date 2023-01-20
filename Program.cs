using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;


namespace SPminiBot{
    class Program{
        private static TelegramBotClient client;

        private static Dictionary<string, int> UsersIDMessageCount = new Dictionary<string, int>{ };

        private static List<string> MessagesConsole = new List<string>();
        private static List<string> MessagesPublic = new List<string>();

        private static int TotalMessageAntiSpam = 35;
        private static int TotalSubcriber;

        private static string NewLog;

        static void Main(){

            client = new TelegramBotClient(Token.token){Timeout = TimeSpan.FromSeconds(11)};
            NewLog =
                @$"Log_{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}.{DateTime.Now.Second}.{DateTime.Now.Millisecond}";

            Console.WriteLine(NewLog);

            client.StartReceiving();

            client.OnMessage += Bot_messages;

            File.WriteAllText(@$"/root/SPminiBOT/Logs/{NewLog}.txt", "");
            File.WriteAllText(@"/root/SPminiBOT/Sub/TotalSubscriber.txt", "0");
            TotalSubcriber = int.Parse(File.ReadAllText(@"/root/SPminiBOT/Sub/TotalSubscriber.txt"));

            Console.ReadKey();
        }

        private static async void Bot_messages(object sender, MessageEventArgs e){
            var msg = e.Message;

            bool IsAdminPanelActive = false;
            string UserName = "";

            string AllSubscribe = File.ReadAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt");

            if (msg == null)
                return;

            MessagesConsole.Add(msg.Text);
            MessagesPublic.Add($"\nСообщение:\n" +
                               $"Время: \"{DateTime.Now}\" \n" +
                               $"Айди человека: \"{msg.From.Id}\"\n" +
                               $"Ник человека: \"{msg.From.Username}\"\n" +
                               $"Чат айди: \"{msg.Chat.Id}\"\n" +
                               $"Сообщение: \"{msg.Text}\"\n");

            string Messagesss = "";

            foreach (var i in MessagesPublic)
                Messagesss = Messagesss + i;

            File.WriteAllText(@$"/root/SPminiBOT/Logs/{NewLog}.txt", Messagesss);


            Console.WriteLine(Messagesss);

            UserName = msg.From.Username;
            if (UserName == ""){
                UsersIDMessageCount.Add($"{msg.From.Id}", 1);
            }

            UserName = $"{msg.From.Id}";

            if (msg != null){
                if (!UsersIDMessageCount.ContainsKey(UserName))
                    UsersIDMessageCount.Add(UserName, 1);
            }

            if (msg.Text == "/start"){
                await client.SendTextMessageAsync(
                    replyMarkup: Panels.MainButtons(),
                    chatId: msg.Chat.Id,
                    text: "Здраствуйте, это высоко-технологический бот телеграм канала СПм\n" +
                          "Снизу есть несколько кнопок для получение информации\n" +
                          "Нажмите их и получите всё, что вам нужно!");
            }

            switch (msg.Text){
                case "Админ Панель":

                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //  msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Выбери одну из команд",
                        replyMarkup: Panels.AdminPanels());

                    break;

                case "Вывести Логи":

                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //   msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    string Messagess = "";

                    foreach (var i in MessagesPublic)
                        Messagess = Messagess + i;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: Messagess);

                    break;

                case "Уведомления":
                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //   msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Выберите одну из кнопок",
                        replyMarkup: Panels.AdminSubPanels());

                    break;

                case "Подписать кого-либо на уведомления":
                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //   msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Введите ID человека");

                    if (msg.Text == "Подписать кого-либо на уведомления"){
                        while (msg.Text == "Подписать кого-либо на уведомления"){
                            Panels.ExitButton();
                            if (msg.Text == "Отмена"){
                                await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "Выберите кнопку",
                                    replyMarkup: Panels.MainButtons());

                                return;
                            }

                            if (MessagesConsole.Last() != "Подписать кого-либо на уведомления"
                                && MessagesConsole.Last() != "Отмена")
                                break;
                        }
                    }

                    string text = File.ReadAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt");
                    File.WriteAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt", text + MessagesConsole.Last() + "&");

                    TotalSubcriber++;
                    File.WriteAllText("/root/SPminiBOT/Sub/TotalSubscriber.txt", TotalSubcriber.ToString());

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Человек записан.");

                    break;

                case "Отписать кого-либо от уведомлений":
                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //   msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Введите ID человека");

                    if (msg.Text == "Отписать кого-либо от уведомлений"){
                        while (msg.Text == "Отписать кого-либо от уведомлений"){
                            Panels.ExitButton();
                            if (msg.Text == "Отмена"){
                                await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "Выберите кнопку",
                                    replyMarkup: Panels.MainButtons());

                                return;
                            }

                            if (MessagesConsole.Last() != "Отписать кого-либо от уведомлений"
                                && MessagesConsole.Last() != "Отмена")
                                break;
                        }
                    }

                    string text2 = File.ReadAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt");
                    if (!text2.Contains(MessagesConsole.Last())){
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Такого в базе данных нет");

                        break;
                    }

                    text2 = text2.Replace(MessagesConsole.Last() + "&", "");
                    File.WriteAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt", text2);
                    TotalSubcriber--;

                    File.WriteAllText(@"/root/SPminiBOT/Sub/TotalSubscriber.txt", $"{TotalSubcriber}");
                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Человек отписан.");

                    break;

                case "Отправить всем уведомление":
                    if (msg.From.Username != "Glither21" &&
                        msg.From.Username != "Trail580" &&
                        //   msg.From.Username != "ketchyppi"&&
                        msg.From.Username != "Smileraze")
                        break;

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Какое сообщение всем отправить ?");

                    if (msg.Text == "Отправить всем уведомление"){
                        while (msg.Text == "Отправить всем уведомление"){
                            Panels.ExitButton();
                            if (msg.Text == "Отмена"){
                                await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "Выберите кнопку",
                                    replyMarkup: Panels.MainButtons());

                                return;
                            }

                            if (MessagesConsole.Last() != "Отправить всем уведомление"
                                && MessagesConsole.Last() != "Отмена")
                                break;
                        }
                    }

                    string TextForMailingList = MessagesConsole.Last();

                    for (int i = 0; i < TotalSubcriber; i++){
                        string UserNames = AllSubscribe.Substring(0, AllSubscribe.IndexOf('&'));

                        await client.SendTextMessageAsync(
                            chatId: UserNames,
                            text: TextForMailingList);

                        AllSubscribe = AllSubscribe.Remove(0, AllSubscribe.IndexOf('&') + 1);

                        if (AllSubscribe == null) break;
                    }

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Уведомление отправленно!");

                    break;
            } //AdminPanel

            switch (msg.Text){
                case "Реклама":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: "Заполните заявку на рекламы, в одном предложение! :\n" +
                                  "1. Ваш ник " +
                                  "\n 2. Что за реклама " +
                                  "\n 3. Сколько вы за неё готовы отдать, минимум 32 ар");

                        if (msg.Text == "Реклама"){
                            while (msg.Text == "Реклама"){
                                Panels.ExitButton();
                                if (msg.Text == "Отмена"){
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: "Выберите кнопку",
                                        replyMarkup: Panels.MainButtons());

                                    return;
                                }

                                if (MessagesConsole.Last() != "Реклама"
                                    && MessagesConsole.Last() != "Отмена")
                                    break;
                            }
                        }

                        string id = msg.From.Username;

                        if (msg.Text != "Отмена" ||
                            msg.Text != "Предложить новость" ||
                            msg.Text != "Реклама" ||
                            msg.Text != "Создатели Телеграмм канала" ||
                            msg.Text != "Назад"){
                            await client.SendTextMessageAsync(
                                chatId: 1548313628,
                                text: $"Новая заявку на рекламу:\n{MessagesConsole.Last()}\nОтправитель: {id}");

                            await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Предложение отправленно. Ожидайте ответа. ");
                        }
                    }

                    break;
                case "Предложить мем":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: "Заполните заявку на мем, в одном предложение! :\n" +
                                  "1. Ваш ник " +
                                  "\n2. Мем как ссылка \n" +
                                  "3. Вы ходите предложить анонимно мем ?");

                        if (msg.Text == "Предложить мем"){
                            while (msg.Text == "Предложить мем"){
                                Panels.ExitButton();
                                if (msg.Text == "Отмена"){
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: "Выберите кнопку",
                                        replyMarkup: Panels.MainButtons());

                                    return;
                                }

                                if (MessagesConsole.Last() != "Предложить мем"
                                    && MessagesConsole.Last() != "Отмена")
                                    break;
                            }
                        }

                        string id = msg.From.Username;

                        if (msg.Text != "Отмена" ||
                            msg.Text != "Предложить новость" ||
                            msg.Text != "Реклама" ||
                            msg.Text != "Предложить мем" ||
                            msg.Text != "Создатели Телеграмм канала" ||
                            msg.Text != "Назад"){
                            await client.SendTextMessageAsync(
                                chatId: 1548313628,
                                text: $"Новая заявку на мем:\n{MessagesConsole.Last()}\nОтправитель: {id}");

                            await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Предложение отправленно. Ожидайте ответа. ");
                        }
                    }

                    break;
                case "Предложить новость":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text:
                            "Заполните заявку на новость, в одном предложение! : \n 1. Ваш ник \n 2. Что за новость \n 3. Когда она произошла/будет проходить");

                        if (msg.Text == "Предложить новость"){
                            Panels.ExitButton();

                            while (msg.Text == "Предложить новость"){
                                if (msg.Text == "Отмена"){
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: "Выберите кнопку",
                                        replyMarkup: Panels.MainButtons());

                                    return;
                                }

                                if (MessagesConsole.Last() != "Предложить новость"
                                    && MessagesConsole.Last() != "Отмена")
                                    break;
                            }
                        }

                        string id = msg.From.Username;

                        if (msg.Text != "Отмена" ||
                            msg.Text != "Предложить новость" ||
                            msg.Text != "Реклама" ||
                            msg.Text != "Создатели Телеграмм канала" ||
                            msg.Text != "Назад"){
                            await client.SendTextMessageAsync(
                                chatId: 1548313628,
                                text: $"Новая заявку на новость:\n{MessagesConsole.Last()}\nОтправитель: {id}");

                            await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Предложение отправленно. Ожидайте ответа. ");
                        }
                    }

                    break;
                case "Создатели Телеграм канала":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){

                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Создали телеграм канал Лига Ачивок & СПмТВ \n" +
                                  "Создал телеграм бота Glither");
                    }

                    break;
            }

            switch (msg.Text){
                case "Ссылки":
                    UsersIDMessageCount[UserName] += 1;

                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            replyMarkup: Panels.LinksButtons(),
                            chatId: msg.Chat.Id,
                            text: "Выберите ссылку");
                    }

                    break;
                case "Информация сервера":
                    UsersIDMessageCount[UserName] += 1;

                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text:
                            "На этом сервере проходят обходы на которых игроки показывают, что они успели сделать за время пребывания на сервере. \n" +
                            "Игроки этого сервера САМИ выбирают президента, который будет руководить жизнью сервера.");
                    }

                    break;
                case "Прочее":
                    UsersIDMessageCount[UserName] += 1;

                    await client.SendTextMessageAsync(
                        replyMarkup: Panels.OtherButtons(),
                        chatId: msg.Chat.Id,
                        text: "Выберите кнопку");
                    break;
            }

            switch (msg.Text){
                case "Назад":
                    await client.SendTextMessageAsync(
                        replyMarkup: Panels.MainButtons(),
                        chatId: msg.Chat.Id,
                        text: "Выберите кнопку");
                    break;

                case "ВК":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "*Клик*",
                                    "https://vk.com/spmini")),
                            chatId: msg.Chat.Id,
                            text: "ВК группа СПм");
                    }

                    break;

                case "СПрадио":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "*Клик*",
                                    "https://spworlds.ru/radio")),
                            chatId: msg.Chat.Id,
                            text: "Сайт СПрадио");
                    }

                    break;

                case "Сайт":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= 10){
                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "*Клик*",
                                    "https://spworlds.ru/spm")),
                            chatId: msg.Chat.Id,
                            text: "Сайт сервера СПм");

                        await client.SendTextMessageAsync(
                            replyMarkup: new InlineKeyboardMarkup(
                                InlineKeyboardButton.WithUrl(
                                    "*Клик*",
                                    "https://spworlds.ru/")),
                            chatId: msg.Chat.Id,
                            text: "Сайт для покупки проходки");
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

            switch (msg.Text){
                case "Настройка уведомлений":

                    if (!AllSubscribe.Contains(msg.From.Id.ToString())){
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Выберите кнопку",
                            replyMarkup: Panels.NotificationsPanels());
                    }
                    else if (AllSubscribe.Contains(msg.From.Id.ToString())){
                        await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "Выберите кнопку",
                            replyMarkup: Panels.UnNotificationsPanels());
                    }

                    break;
                case "Подписаться на уведомления":
                    string text2 = File.ReadAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt");

                    if (text2.Contains(msg.From.Id.ToString())) break;

                    File.WriteAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt", AllSubscribe + msg.Chat.Id + "&");
                    TotalSubcriber++;

                    File.WriteAllText(@"/root/SPminiBOT/Sub/TotalSubscriber.txt", $"{TotalSubcriber}");

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Вы успешно подписались на уведомления!",
                        replyMarkup: Panels.UnNotificationsPanels());

                    break;
                case "Отписаться от уведомлений":
                    string text = File.ReadAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt");
                    text = text.Replace(msg.From.Id + "&", "");
                    File.WriteAllText(@"/root/SPminiBOT/Sub/SubscriptionList.txt", text);
                    TotalSubcriber--;

                    File.WriteAllText(@"/root/SPminiBOT/Sub/TotalSubscriber.txt", $"{TotalSubcriber}");

                    await client.SendTextMessageAsync(
                        chatId: msg.Chat.Id,
                        text: "Вы отписались от уведомлений, ждём вас вновь!",
                        replyMarkup: Panels.NotificationsPanels());

                    break;
                case "Сделать запрос на отправку уведомления":
                    UsersIDMessageCount[UserName] += 1;
                    if (UsersIDMessageCount[UserName] <= TotalMessageAntiSpam){
                        await client.SendTextMessageAsync(
                            chatId: msg.From.Id,
                            text: "Заполните заявку на рекламы, в одном предложение! :\n" +
                                  "1. Ваш ник " +
                                  "\n 2. Что за уведомление " +
                                  "\n 3. Напишите текст для уведомления" +
                                  "\nПлатное уведомление - 64АР. Eсли это новость, бесплатно. ");

                        if (msg.Text == "Сделать запрос на отправку уведомления"){
                            while (msg.Text == "Сделать запрос на отправку уведомления"){
                                Panels.ExitButton();
                                if (msg.Text == "Отмена"){
                                    await client.SendTextMessageAsync(
                                        chatId: msg.Chat.Id,
                                        text: "Выберите кнопку",
                                        replyMarkup: Panels.MainButtons());

                                    return;
                                }

                                if (MessagesConsole.Last() != "Сделать запрос на отправку уведомления"
                                    && MessagesConsole.Last() != "Отмена")
                                    break;
                            }
                        }

                        string id = msg.From.Username;

                        if (msg.Text != "Отмена" ||
                            msg.Text != "Предложить новость" ||
                            msg.Text != "Реклама" ||
                            msg.Text != "Создатели Телеграмм канала" ||
                            msg.Text != "Назад"){
                            await client.SendTextMessageAsync(
                                chatId: 1548313628,
                                text: $"Новый запрос на уведомление:\n{MessagesConsole.Last()}\nОтправитель: {id}");

                            await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "Предложение отправленно. Ожидайте ответа. ");
                        }


                    }

                    break;

            }
        }
    }
}