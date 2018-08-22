using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PT.Telegram
{
    class Program
    {
        private static TelegramBotClient bot;

        static void Main(string[] args)
        {
            Test().GetAwaiter().GetResult();
        }

        static async Task Test()
        {
            string token = ConfigurationManager.AppSettings["telegramToken"];
            bot = new TelegramBotClient(token);
            var me = await bot.GetMeAsync();
            Console.WriteLine($"My name is {me.FirstName}");
            bot.OnMessage += BotOnMessageReceived;

            bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text) return;

            Console.WriteLine($"Received: {e.Message.Text}");

            using (var db = new PtContext())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.TelegramChatId == message.Chat.Id);
                if (user == null)
                {
                    await bot.SendTextMessageAsync(message.Chat.Id, "You are not registered yet. Use the following ID to register:");
                    await bot.SendTextMessageAsync(message.Chat.Id, message.Chat.Id.ToString());
                    return;
                }
            }

            switch (message.Text.Split(' ').First().ToLower())
            {
                case "/workout":
                    await bot.SendTextMessageAsync(message.Chat.Id, GetNextWorkout(message.Chat.Id));
                    break;
                case "/done":
                    FinishWorkout(message.Chat.Id);
                    await bot.SendTextMessageAsync(message.Chat.Id, "Workout marked as finished, good job!");
                    break;
                case "/motivation":
                    await bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                    const string file = @"Files/rocky.jpg";
                    using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await bot.SendPhotoAsync(message.Chat.Id, fileStream, "Motivation");
                    }
                    break;
                case "menu":
                case "/menu":
                case "help":
                case "/help":
                    const string help = @"Usage:
/workout - Get next workout
/done - Mark workout as done
/motivation - Get some motivation!
/help - get some help..";

                    await bot.SendTextMessageAsync(
                        message.Chat.Id,
                        help,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;
                default:
                    await bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "What are you trying to do? (/help)");
                    break;
            }
        }

        private static void FinishWorkout(long chatId)
        {
            using (var db = new PtContext())
            {
                var user = db.Users.Include(u => u.Workouts)
                    .Single(u => u.TelegramChatId == chatId);
                var nextWorkout = user.Workouts
                    .Where(t => t.Date == DateTime.Now.Date)
                    .OrderBy(t => t.Date).FirstOrDefault();

                if (nextWorkout == null) return;
                
                nextWorkout.Finished = true;
                db.SaveChanges();
            }
        }

        private static string GetNextWorkout(long chatId)
        {
            using (var db = new PtContext())
            {
                var user = db.Users.Include(u => u.Workouts)
                        .ThenInclude(t => t.WorkoutType)
                    .Single(u => u.TelegramChatId == chatId);

                var firstNextWorkout = user.Workouts
                    .Where(t => t.Date >= DateTime.Now.Date)
                    .OrderBy(t => t.Date)
                    .FirstOrDefault();

                if (firstNextWorkout == null) return "No upcoming workout!";
                if (firstNextWorkout.Date == DateTime.Now.Date && !firstNextWorkout.Finished)
                    return $"Todays workout: {firstNextWorkout.WorkoutType.Name}";

                if (firstNextWorkout.Date == DateTime.Now.Date && firstNextWorkout.Finished)
                {
                    var nextWorkout = user.Workouts
                        .Where(t => t.Date > firstNextWorkout.Date)
                        .OrderBy(t => t.Date).FirstOrDefault();

                    var todaysFinished = $"Todays workout is finished, good job! ({firstNextWorkout.WorkoutType.Name}). ";

                    return nextWorkout != null ? 
                        $"{todaysFinished} Next workout: {nextWorkout.WorkoutType.Name} is at: {nextWorkout.Date.ToShortDateString()}" :
                        $"{todaysFinished} No upcoming workout scheduled.";
                }

                return $"Next workout: {firstNextWorkout.WorkoutType.Name} is at {firstNextWorkout.Date.Date.ToShortDateString()}";
            }
        }
    }
}
