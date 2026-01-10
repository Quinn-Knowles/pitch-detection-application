using System.Text;
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

namespace pitch_detection_application
{
    public partial class MainWindow : Window
    {
        private const string SETTINGS_FILE = "src/last_settings.txt";

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void Monitor_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();

            var args = $"src/Monitor.py --max {MaxPitchBox.Text} --min {MinPitchBox.Text}";

            if (SentenceMonitorBox.IsChecked == true) args += " --sentence-monitor";
            if (OutOfRangeBox.IsChecked == true) args += " --out-of-range";
            if (SelfMuteBox.IsChecked == true) args += " --self-mute";
            if (ResonanceBox.IsChecked == true) args += " --resonance";
            if (ScoreBox.IsChecked == true) args += " --score";
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }
                private void SaveSettings()
        {
            Directory.CreateDirectory("src");

            using var writer = new StreamWriter(SETTINGS_FILE);
            writer.WriteLine($"max:{MaxPitchBox.Text}");
            writer.WriteLine($"min:{MinPitchBox.Text}");
            writer.WriteLine($"sentence_monitor:{SentenceMonitorBox.IsChecked}");
            writer.WriteLine($"out_of_range:{OutOfRangeBox.IsChecked}");
            writer.WriteLine($"self_mute:{SelfMuteBox.IsChecked}");
            writer.WriteLine($"resonance:{ResonanceBox.IsChecked}");
            writer.WriteLine($"score:{ScoreBox.IsChecked}");
        }

        private void LoadSettings()
        {
            if (!File.Exists(SETTINGS_FILE)) return;

            foreach (var line in File.ReadAllLines(SETTINGS_FILE))
            {
                var parts = line.Split(':', 2);
                if (parts.Length != 2) continue;

                switch (parts[0])
                {
                    case "max": MaxPitchBox.Text = parts[1]; break;
                    case "min": MinPitchBox.Text = parts[1]; break;
                    case "sentence_monitor": SentenceMonitorBox.IsChecked = bool.Parse(parts[1]); break;
                    case "out_of_range": OutOfRangeBox.IsChecked = bool.Parse(parts[1]); break;
                    case "self_mute": SelfMuteBox.IsChecked = bool.Parse(parts[1]); break;
                    case "resonance": ResonanceBox.IsChecked = bool.Parse(parts[1]); break;
                    case "score": ScoreBox.IsChecked = bool.Parse(parts[1]); break;
                }
            }
        }
    }
}
