using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPminiBot{
    public class Panels{
        public static IReplyMarkup MainButtons(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton> {new KeyboardButton{Text = "Ссылки"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Информация сервера"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Мемы"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Прочее"}}
                }
            };
        }

        public static IReplyMarkup LinksButtons(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton> {new KeyboardButton{Text = "Сайт"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "СПрадио"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "ВК"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Ссылки создателей"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Назад"}}
                }
            };
        }

        public static IReplyMarkup OtherButtons(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton> {new KeyboardButton{Text = "Реклама"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Предложить новость"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Создатели Телеграм канала"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Назад"}}
                }
            };
        }
    }
}