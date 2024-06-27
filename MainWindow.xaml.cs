using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace NonPCGame_Discord
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void ChangeLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string cultureName)
            {
                SetLanguage(cultureName);
                UpdateUI();
            }
        }

        private void SetLanguage(string cultureName)
        {
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Properties.Resources.Culture = culture;
        }

        private void UpdateUI()
        {
            // UI要素のテキストを更新
            this.Title = Properties.Resources.Title_MainWindow;
            menuItem_Setting.Header = Properties.Resources.Setting;
            menuItem_Setting_Language.Header = Properties.Resources.Setting_Language;

            // 言語選択のMenuItemにチェックマークをつける
            foreach (MenuItem item in menuItem_Setting_Language.Items)
            {
                if (item.Tag is string cultureName)
                {
                    item.IsChecked = cultureName == Thread.CurrentThread.CurrentUICulture.Name;
                }
            }
        }
    }
}
