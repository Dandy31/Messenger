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
using System.IO;
using System.Windows.Media.Animation;

namespace WpfAppExamMessenger
{
    
    class Person
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string LastSms { get; set; }
        public string PathFile { get; set; }
        public string LastTime { get; set; }
        public List<ContentControl> myListBox { get; set; }
    }
    public partial class MainWindow : Window
    {
        List<Person> people;
        int chosse_index = 0;
        public MainWindow()
        {
            InitializeComponent();
            people = new List<Person>()
            {
                new Person(){Name = "Андрей", ImagePath = "D:\\icon\\Telegram Icon\\users-1.png", LastSms="", LastTime=""},
                new Person(){Name = "Бородач", ImagePath = "D:\\icon\\Telegram Icon\\users-10.png", LastSms="", LastTime=""},
                new Person(){Name = "Вика", ImagePath = "D:\\icon\\Telegram Icon\\users-3.png", LastSms="", LastTime=""},
                new Person(){Name = "Настя", ImagePath = "D:\\icon\\Telegram Icon\\users-5.png", LastSms="", LastTime=""},
                new Person(){Name = "Соня", ImagePath = "D:\\icon\\Telegram Icon\\users-16.png", LastSms="", LastTime=""},
                new Person(){Name = "Арина", ImagePath = "D:\\icon\\Telegram Icon\\users-14.png", LastSms="", LastTime=""},
                new Person(){Name = "Ботан", ImagePath = "D:\\icon\\Telegram Icon\\users-6.png", LastSms="", LastTime=""},
                new Person(){Name = "Назар", ImagePath = "D:\\icon\\Telegram Icon\\users-15.png", LastSms="", LastTime=""},
            };
            for (int i = 0; i < people.Count; i++)
            {
                people[i].PathFile = $"D:\\Visual Projects\\WpfAppExamMessenger\\WpfAppExamMessenger\\{people[i].Name}.txt";
                peopleList.Items.Add(people[i]);
            }
           


            for (int i = 0; i < peopleList.Items.Count; i++)
            {
                searchControl.Add((Person)peopleList.Items[i]);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Пока будут использоваться тестовые данные
            if (LoginBox.Text == "admin" && PasswordBox.Password == "12345")
            {
                //Если логин и пароль верные, то переходим на другой экран
                LoginScreen.Visibility = Visibility.Hidden;
            }
            else
            {
                //Иначе выводим сообщение об ошибке авторизации
                LoginMessageBlock.Text = "Wrong login or password!";
                LoginMessageBlock.Visibility = Visibility.Visible;
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("settings");
        }


        private void peopleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < peopleList.Items.Count; i++)
            {
                if (i == peopleList.SelectedIndex)
                {
                    smsList.Items.Clear();

                    if (people[i].myListBox != null)
                    {
                        for (int j = 0; j < people[i].myListBox.Count; j++)
                        {
                            smsList.Items.Add(people[i].myListBox[j]);
                        }
                    }
                    else
                    {
                        smsList.Items.Clear();
                        people[i].myListBox = new List<ContentControl>();
                    }
                    chosse_index = i;
                }
            }

        }

        ContentControl text;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(peopleList.SelectedIndex >= 0)
            {
                text = new ContentControl();
                text.Content = sendText.Text;
                text.Margin = new Thickness { Left = 0 };
                text.FontSize = 15;
                text.Foreground = Brushes.Gray;
                for (int i = 0; i < peopleList.Items.Count; i++)
                {
                    if (i == peopleList.SelectedIndex)
                    {
                        people[i].LastSms = sendText.Text;
                        peopleList.Items.RemoveAt(i);
                        peopleList.Items.Insert(i, people[i]);
                        peopleList.SelectedItem = peopleList.Items[i];

                        smsList.Items.Add(text);
                        people[i].myListBox.Add((ContentControl)smsList.Items[smsList.Items.Count - 1]);
                        using (StreamWriter sw = new StreamWriter(people[i].PathFile))
                        {
                            sw.WriteLine($"{sendText.Text}");
                        };

                    }
                }


                DoubleAnimation textAnimation = new DoubleAnimation();

                textAnimation.From = 0;
                textAnimation.To = 10;

                textAnimation.To += sendText.Text.Length*5 + sendText.Text.Length * 2;

                textAnimation.Duration = TimeSpan.FromMilliseconds(180);
                ((ContentControl)smsList.Items[smsList.Items.Count - 1]).BeginAnimation(ContentControl.WidthProperty, textAnimation);

                sendText.Text = "";
                text = null;

                
            }
        }

        private void sendText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (peopleList.SelectedIndex >= 0)
                {
                    text = new ContentControl();
                    text.Content = sendText.Text;
                    text.Margin = new Thickness { Left = 0 };
                    text.FontSize = 15;
                    text.Foreground = Brushes.Gray;
                    for (int i = 0; i < peopleList.Items.Count; i++)
                    {
                        if (i == peopleList.SelectedIndex)
                        {
                            people[i].LastSms = sendText.Text;
                            peopleList.Items.RemoveAt(i);
                            peopleList.Items.Insert(i, people[i]);
                            peopleList.SelectedItem = peopleList.Items[i];

                            smsList.Items.Add(text);
                            people[i].myListBox.Add((ContentControl)smsList.Items[smsList.Items.Count - 1]);
                            using (StreamWriter sw = new StreamWriter(people[i].PathFile))
                            {
                                sw.WriteLine($"{sendText.Text}");
                            };

                            
                        }
                    }

                    DoubleAnimation textAnimation = new DoubleAnimation();

                    textAnimation.From = 0;
                    textAnimation.To = 10;

                    textAnimation.To += sendText.Text.Length * 5 + sendText.Text.Length * 2;

                    textAnimation.Duration = TimeSpan.FromMilliseconds(200);
                    ((ContentControl)smsList.Items[smsList.Items.Count - 1]).BeginAnimation(ContentControl.WidthProperty, textAnimation);

                    sendText.Text = "";
                    text = null;
                }
            }
        }

        List<Person> searchControl = new List<Person>();


        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock text = (TextBlock)e.Source;
            text.Text = DateTime.Now.Hour.ToString() +":"+ DateTime.Now.Minute.ToString();
            e.Source = text;
            if (DateTime.Now.Minute < 10 && DateTime.Now.Hour < 10)
                people[chosse_index].LastTime = "0" + DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString();
            else if (DateTime.Now.Minute < 10 && DateTime.Now.Hour > 10)
                people[chosse_index].LastTime = DateTime.Now.Hour.ToString() + ":0" + DateTime.Now.Minute.ToString();
            else if (DateTime.Now.Minute > 10 && DateTime.Now.Hour < 10)
                people[chosse_index].LastTime = "0" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            else
                people[chosse_index].LastTime = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
        }

        private void Rectangle_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
           

            for (int i = 0; i < peopleList.Items.Count; i++)
            {
                if (i == peopleList.SelectedIndex)
                {
                    smsList.Items.Clear();

                    if (people[i].myListBox != null)
                    {
                        for (int j = 0; j < people[i].myListBox.Count; j++)
                        {
                            smsList.Items.Add(people[i].myListBox[j]);
                        }
                    }
                    else
                    {
                        smsList.Items.Clear();
                        people[i].myListBox = new List<ContentControl>();
                    }
                }
            }

        }
    }
}
