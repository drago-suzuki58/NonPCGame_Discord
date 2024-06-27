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
using Discord;

namespace NonPCGame_Discord
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool isInitializationComplete = false;
        public Discord.Discord discord;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += MainWindow_Closing;

            Initialize();

            Task.Run(() =>
            {
                while (true)
                {
                    discord.RunCallbacks();
                    Thread.Sleep(1000); // 1秒ごとにコールバックを実行
                }
            });
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isInitializationComplete)
            {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.confirmation_BeforeInit, Properties.Resources.confirmation, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    MainWindow_Closed();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.confirmation_common, Properties.Resources.confirmation, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    MainWindow_Closed();
                }
            }
        }

        private void MainWindow_Closed()
        {
            // Discordオブジェクトの解放
            discord.Dispose();
        }

        private void Initialize()
        {
            UpdateUI();
            discord = DiscordInit();
            DiscordActivity(discord);
            isInitializationComplete = true;
            Console.WriteLine("Initialization complete.");
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

        private Discord.Discord DiscordInit()
        {
            Discord.Discord discord = new Discord.Discord(1255625655844995123, (ulong)CreateFlags.Default);

            return discord;
        }

        private void DiscordActivity(Discord.Discord discord)
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Activity
            {
                // テストコード(WIP)
                Details = "Details",
                State = "State",
            };

            activityManager.UpdateActivity(activity, (result) =>
            {
                if (result == Result.Ok)
                {
                    Console.WriteLine("Activity updated successfully.");
                    // アクティビティの更新に成功した場合のみ実行したい処理を記述(WIP)
                }
                else
                {
                    Console.WriteLine("Activity failed to update.");
                }
            });
        }
    }
}
