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
using workingWithSql;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fullTable.DataContext = db.DatabaseOutput();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            colorTable.DataContext = db.OutputAllColor();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            titleTable.DataContext = db.OutputAllTitle();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            maxCaloric.Text = db.MaximumCaloric().ToString();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            minCaloric.Text= db.MinimumCaloric().ToString();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            avgCaloric.Text = db.AverageCaloric().ToString();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            CountV.Text = db.NumberVegetables().ToString();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            countF.Text = db.NumberFruits().ToString();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            countTable.DataContext = db.PrintCountVAF();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if(colorTextBox.Text != String.Empty)
                countVAF.Text = db.CountVegetablesAndFruits(colorTextBox.Text).ToString();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if(Min.Text != String.Empty)
                caloricMinTable.DataContext = db.PrintLessCaloric(Convert.ToInt32(Min.Text));
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (Max.Text != String.Empty)
                caloricMaxTable.DataContext = db.PrintMoreCaloric(Convert.ToInt32(Max.Text));
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (MinB.Text != String.Empty && MaxB.Text != String.Empty)
                caloricBetweenTable.DataContext = db.PrintBetweenCaloric(Convert.ToInt32(MinB.Text), Convert.ToInt32(MaxB.Text));
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            colorYRTable.DataContext = db.Print();
        }
    }
}
