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
using Microsoft.Win32;
namespace BellRingers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            this.Reset();
        }
        public void Reset()
        {
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            towerNames.Items.Clear();
            foreach (string towerName in towers)
            {
                towerNames.Items.Add(towerName);
            }
            towerNames.Text = towerNames.Items[0] as string;
            methods.Items.Clear();
            CheckBox method = null;
            foreach (string methodName in ringingMethods)
            {
                method = new CheckBox();
                method.Margin = new Thickness(0, 0, 0, 10);
                method.Content = methodName;
                methods.Items.Add(method);
            }
            IsCaptain.IsChecked = false;
            novice.IsChecked = true;
        }
        private string[] towers = { "Great Shevington", "Little Mudford","Upper Gumtree", "Downley Hatch" };
        private string[] ringingMethods = { "Plain Bob", "Reverse Canterbury","Grandsire", "Stedman", "Kent Treble Bob", "Old Oxford Delight","Winchendon Place", "Norwich Surprise", "Crayford Little Court" };
       
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string nameAndTower = String.Format("Member name: {0} {1} from the tower at {2} rings the following methods:",firstName.Text, lastName.Text, towerNames.Text);
            StringBuilder details = new StringBuilder();
            details.AppendLine(nameAndTower);
            foreach (CheckBox cb in methods.Items)
            {
                if (cb.IsChecked.Value)
                {
                    details.AppendLine(cb.Content.ToString());
                }
            }
            MessageBox.Show(details.ToString(), "Member Information");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show
                       ("Are you sure you want to quit",
                         "Confirm",
                          MessageBoxButton.YesNo,
                          MessageBoxImage.Question,
                          MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
        }

        private void NewMember_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
            saveMember.IsEnabled = true;
            firstName.IsEnabled = true;
            lastName.IsEnabled = true;
            towerNames.IsEnabled = true;
            IsCaptain.IsEnabled = true;
            memberSince.IsEnabled = true;
            yearsExperience.IsEnabled = true;
            methods.IsEnabled = true;
            clear.IsEnabled = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveMember_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "txt";
            saveDialog.AddExtension = true;
            saveDialog.FileName = "Members";
            saveDialog.InitialDirectory = @"C:\Users\Public\Public Documents\";
            saveDialog.OverwritePrompt = true;
            saveDialog.Title = "Bell Ringers";
            saveDialog.ValidateNames = true;
            if (saveDialog.ShowDialog().Value)
            {
                using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                {

                    writer.WriteLine("First Name: {0}", firstName.Text);

                    writer.WriteLine("Last Name: {0}", lastName.Text);

                    writer.WriteLine("Tower: {0}", towerNames.Text);

                    writer.WriteLine("Captain: {0}", IsCaptain.IsChecked.ToString());

                    writer.WriteLine("Member Since: {0}", memberSince.Text);

                    writer.WriteLine("Methods: ");

                    foreach (CheckBox cb in methods.Items)

                    {

                        if (cb.IsChecked.Value)

                        {

                            writer.WriteLine(cb.Content.ToString());

                        }

                    }

                    MessageBox.Show("Member details saved", "Saved");

                }
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ClearName_Click(object sender, RoutedEventArgs e)
        {
            firstName.Clear();
            lastName.Clear();
        }
    }
}
