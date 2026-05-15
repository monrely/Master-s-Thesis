using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MusicForge;

public partial class MainWindow : Window
{
    
    const string DefaultProgram =
@"bpm 70

instrument :sine #it's default

play :c4, 0.4
sleep 0.6

play :d4, 0.4
sleep 0.5

play :d#4, 1
sleep 1

play :d#4, 1
sleep 1

play :d#4, 0.4
sleep 0.5

play :d#4, 0.5
sleep 0.5

play :d4, 0.4
sleep 0.5

play :d#4, 0.4
sleep 0.5

play :f4, 0.4
sleep 0.5

play :d#4, 0.4
sleep 0.5

play :d4, 1
sleep 1

play :c4, 1.2
sleep 1
";

    public MainWindow()
    {
        InitializeComponent();
        CodeBox.Text = DefaultProgram;
        LoadExamplesList();
    }


    void LoadExamplesList()
    {
        ExamplesCombo.Items.Clear();
        string dir = Path.Combine(System.AppContext.BaseDirectory, "Examples");
        if (!Directory.Exists(dir)) return;
        foreach (var path in Directory.EnumerateFiles(dir, "*.mf"))
        {
            ExamplesCombo.Items.Add(new ComboBoxItem
            {
                Content = Path.GetFileNameWithoutExtension(path),
                Tag = path
            });
        }
    }

    void ExamplesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ExamplesCombo.SelectedItem is ComboBoxItem item && item.Tag is string path)
        {
            CodeBox.Text = File.ReadAllText(path);
            StatusBar.Text = "Loaded " + Path.GetFileName(path);
        }
    }


    void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            
            Scanner.input = CodeBox.Text;
            Scanner.index = 0;
            Scanner.Next();
            Scanner.Scan();

            
            Block program = Parser.Parse();


            Interpreter.Reset();
            program.Interpret();


            Audio.Play();

            StatusBar.Text =
                $"Playing {Interpreter.events.Count} note(s) at {Interpreter.bpm} bpm.";
        }
        catch (System.Exception ex)
        {
            StatusBar.Text = "Error: " + ex.Message;
        }
    }

    void StopButton_Click(object sender, RoutedEventArgs e)
    {
        Audio.Stop();
        StatusBar.Text = "Stopped.";
    }
}
