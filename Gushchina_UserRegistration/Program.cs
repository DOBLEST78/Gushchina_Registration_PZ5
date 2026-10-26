using Gushchina_PasswordSecurity;
using Gushchina_UserRegistration.Model;
using System;
using System.Linq;
using System.Text;

namespace Gushchina_UserRegistration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Создание новой учетной записи пользователя");

            Console.Write("Введите ФИО пользователя: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон пользователя: ");
            string phoneText = Console.ReadLine();

            Console.Write("Введите электронную почту пользователя: ");
            string email = Console.ReadLine();

            Console.Write("Введите дату рождения в формате ДД.ММ.ГГГГ: ");
            string birthDateText = Console.ReadLine();

            Console.Write("Введите предпочтения пользователя: ");
            string preferences = Console.ReadLine();

            Console.Write("Введите логин пользователя: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль пользователя: ");
            string password = Console.ReadLine();

            long phone;
            DateTime birthDate;

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                !long.TryParse(phoneText, out phone) ||
                !DateTime.TryParse(birthDateText, out birthDate))
            {
                Console.WriteLine("Введены некорректные данные.");
                Console.ReadKey();
                return;
            }

            try
            {
                RestaurantDataEntities context = DatabaseConnector.GetRestaurantContext();

                if (context.UserLogs.Any(x => x.Login == login))
                {
                    Console.WriteLine("Пользователь с таким логином уже существует.");
                    Console.ReadKey();
                    return;
                }

                byte clientId = context.Clients.Any()
                    ? (byte)(context.Clients.Max(x => x.ClientId) + 1)
                    : (byte)1;

                byte userId = context.UserLogs.Any()
                    ? (byte)(context.UserLogs.Max(x => x.UserId) + 1)
                    : (byte)1;

                string passwordHash = PasswordEncoder.CreatePasswordHash(password);

                Client client = new Client
                {
                    ClientId = clientId,
                    FullName = fullName,
                    Phone = phone,
                    Email = email,
                    BirthDate = birthDate,
                    Preferences = preferences
                };

                UserLog user = new UserLog
                {
                    UserId = userId,
                    ClientId = clientId,
                    Login = login,
                    PasswordHash = passwordHash
                };

                context.Clients.Add(client);
                context.SaveChanges();

                context.UserLogs.Add(user);
                context.SaveChanges();

                Console.WriteLine("Хешированный пароль пользователя: " + passwordHash);
                Console.WriteLine("Учетная запись добавлена.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Ошибка: " + exception.Message);
            }

            Console.ReadKey();
        }
    }
}
