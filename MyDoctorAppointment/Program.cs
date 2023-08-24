using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using MyDoctorAppointment.Service.ViewModels;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using Formatting = System.Xml.Formatting;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }         
    }

    public static class Program
    {
        static void Main(string[] args)
        {
            List<DoctorViewModel> doctors = new List<DoctorViewModel>();

            while (true)
            {
                DoctorViewModel doctor = new DoctorViewModel();

                Console.Write("Введите имя доктора: ");
                doctor.Name = Console.ReadLine();

                Console.Write("Введите фамилию доктора: ");
                doctor.Surname = Console.ReadLine();

                Console.Write("Введите номер телефона доктора: ");
                doctor.Phone = Console.ReadLine();

                Console.Write("Введите адрес электронной почты доктора: ");
                doctor.Email = Console.ReadLine();

                Console.Write("Введите тип доктора: ");
                doctor.DoctorType = Console.ReadLine();

                Console.Write("Введите опыт доктора (в годах): ");
                if (byte.TryParse(Console.ReadLine(), out byte experience))
                {
                    doctor.Experiance = experience;
                }
                else
                {
                    Console.WriteLine("Ошибка: Опыт должен быть числом.");
                    continue;
                }

                Console.Write("Введите зарплату доктора: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal salary))
                {
                    doctor.Salary = salary;
                }
                else
                {
                    Console.WriteLine("Ошибка: Зарплата должна быть числом.");
                    continue;
                }

                doctors.Add(doctor);

                Console.Write("Выберите формат сохранения (json/xml): ");
                string format = Console.ReadLine();


                if (format.ToLower() == "json")
                {
                    string fileName = "D:\\Запись\\материалы\\MyDoctorAppointment (3)\\MyDoctorAppointment\\DoctorAppointment.Data\\MockedDatabase\\doctors.json";
                    SaveToJson(doctors, fileName);
                    Console.WriteLine("Данные успешно сохранены в JSON файл.");
                }
                else if (format.ToLower() == "xml")
                {
                    string fileName = "D:\\Запись\\материалы\\MyDoctorAppointment (3)\\MyDoctorAppointment\\DoctorAppointment.Data\\MockedDatabase\\doctors.xml";
                    SaveToXml(doctors, fileName);
                    Console.WriteLine("Данные успешно сохранены в XML файл.");
                }
                else
                {
                    Console.WriteLine("Неподдерживаемый формат сохранения. Поддерживаются только json и xml.");
                }
            }
            // Сохранение данных в JSON файл
            static void SaveToJson(List<DoctorViewModel> doctors, string fileName)
            {
                string json = JsonConvert.SerializeObject(doctors, (Newtonsoft.Json.Formatting)Formatting.Indented);
                File.WriteAllText(fileName, json);
            }

            // Сохранение данных в XML файл
            static void SaveToXml(List<DoctorViewModel> doctors, string fileName)
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  "
                };

                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    var serializer = new XmlSerializer(typeof(List<DoctorViewModel>));
                    serializer.Serialize(writer, doctors);
                }
            }
        }
    }
}
