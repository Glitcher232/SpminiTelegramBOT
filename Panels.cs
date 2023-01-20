using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace SPminiBot{
    public class Panels{
        public static IReplyMarkup MainButtons(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton> {new KeyboardButton{Text = "Ссылки"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Информация сервера"}},
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
                    new List<KeyboardButton> {new KeyboardButton{Text = "Предложить мем"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Предложить новость"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Настройка уведомлений"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Создатели Телеграм канала"}},
                    new List<KeyboardButton> {new KeyboardButton{Text = "Назад"}}
                }
            };
        }
        
        public static IReplyMarkup ExitButton(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton> {new KeyboardButton{Text = "Отмена"}}
                }
            };
        }

        public static IReplyMarkup AdminPanels(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton>{new KeyboardButton{Text = "Вывести Логи"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Уведомления"}},
		            new List<KeyboardButton>{new KeyboardButton{Text = "Назад"}}
                }
            };
        }
        
        public static IReplyMarkup AdminSubPanels(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton>{new KeyboardButton{Text = "Отправить всем уведомление"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Подписать кого-либо на уведомления"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Отписать кого-либо от уведомлений"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Назад"}}
                }
            };
        }

        public static IReplyMarkup NotificationsPanels(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton>{new KeyboardButton{Text = "Подписаться на уведомления"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Сделать запрос на отправку уведомления"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Назад"}}
                }
            };
        }
        
        public static IReplyMarkup UnNotificationsPanels(){
            return new ReplyKeyboardMarkup{
                Keyboard = new List<List<KeyboardButton>>{
                    new List<KeyboardButton>{new KeyboardButton{Text = "Отписаться от уведомлений"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Сделать запрос на отправку уведомления"}},
                    new List<KeyboardButton>{new KeyboardButton{Text = "Назад"}}
                }
            };
        }
    }
}