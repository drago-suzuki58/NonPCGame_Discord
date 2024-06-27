using System.Globalization;
using System.Threading;
using System.Windows;

namespace NonPCGame_Discord
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // システムのカルチャを取得
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;

            // アプリケーションのカルチャを設定
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;

            // リソースファイルのカルチャを更新
            NonPCGame_Discord.Properties.Resources.Culture = currentCulture;
        }
    }
}
