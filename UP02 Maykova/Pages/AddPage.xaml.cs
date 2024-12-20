using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UP02_Maykova.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        Partners _currentPartners = new Partners();
        public AddPage(Partners currentPartners, bool flag)
        {
            InitializeComponent();
            if (flag)
            {
                ButtonSave.Visibility = Visibility.Visible;
                ButtonRedact.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonSave.Visibility = Visibility.Hidden;
                ButtonRedact.Visibility = Visibility.Visible;
            }
            if (currentPartners != null)
            {
                _currentPartners = currentPartners;
                TypeBox.SelectedIndex = currentPartners.Type - 1;
            }
            DataContext = _currentPartners;
            var context = Entities.GetContext();
            TypeBox.ItemsSource = context.Partner_type.Distinct().ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text) || string.IsNullOrEmpty(TypeBox.Text) || string.IsNullOrEmpty(RatingBox.Text) || string.IsNullOrEmpty(TextBoxAdress.Text) || string.IsNullOrEmpty(TextBoxAdress.Text) || string.IsNullOrEmpty(TextBoxSecond.Text) || string.IsNullOrEmpty(TextBoxFirst.Text) || string.IsNullOrEmpty(TextBoxThird.Text) || string.IsNullOrEmpty(TextBoxPhone.Text) || string.IsNullOrEmpty(TextBoxEmail.Text))
            {
                MessageBox.Show("Заполните все вышеуказанные поля!");
                return;
            }
            using (var database = new Entities())
            {
                var name = database.Partners
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Name == TextBoxName.Text);
                if (name != null)
                {
                    MessageBox.Show("Такой партнёр уже существует");
                    return;
                }
            }
            if (int.Parse(RatingBox.Text) < 0 || RatingBox.Text.Contains(".") || RatingBox.Text.Contains(","))
            {
                MessageBox.Show("Рэйтинг обязан быты целым положительным числом");
                return;
            } // выводим сообщение

            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите Добавить/Сохранить эти данные?", "Подтвержение закрытия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Entities db = new Entities();
                Partners PartnerObject = new Partners
                {
                    Name = TextBoxName.Text,
                    Type = _currentPartners.Type = TypeBox.SelectedIndex + 1,
                    Rating = int.Parse(RatingBox.Text),
                    Adress = TextBoxAdress.Text,
                    Director_name = TextBoxSecond.Text,
                    Director_middle_name = TextBoxFirst.Text,
                    Director_last_name = TextBoxThird.Text,
                    Phone = TextBoxPhone.Text,
                    Email = TextBoxEmail.Text,
                };
                db.Partners.Add(PartnerObject);
                db.SaveChanges();
                MessageBox.Show("Пользователь Добавлен");
                NavigationService.GoBack();

            }
        }

        private void ButtonRedact_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPartners.Name))
                errors.AppendLine("Укажите Название компании!");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentPartners.Rating)) || _currentPartners.Rating < 0)
                errors.AppendLine("Укажите укажите правильный рейтинг!");
            if ((_currentPartners.Type == 0) || (TypeBox.Text == ""))
                errors.AppendLine("Выберите тип компании!");
            else
                _currentPartners.Type = TypeBox.SelectedIndex + 1;
            if (string.IsNullOrWhiteSpace(_currentPartners.Director_name))
                errors.AppendLine("Укажите Фамилию!");
            if (string.IsNullOrWhiteSpace(_currentPartners.Director_middle_name))
                errors.AppendLine("Укажите Имя!");
            if (string.IsNullOrWhiteSpace(_currentPartners.Director_last_name))
                errors.AppendLine("Укажите Отчество!");
            if (string.IsNullOrWhiteSpace(_currentPartners.Adress))
                errors.AppendLine("Укажите Адрес!");
            if (string.IsNullOrWhiteSpace(_currentPartners.Phone))
                errors.AppendLine("Укажите Контактный телефон!");
            if (string.IsNullOrWhiteSpace(_currentPartners.Email))
                errors.AppendLine("Укажите адрес электронной почты!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //Изменяем объект Partners
            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            NavigationService.GoBack();


        }

    }
}
