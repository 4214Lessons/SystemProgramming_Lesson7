using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Source;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }





    private Task SomeMethod()
    {
        // 
        // 
        //

        return Task.CompletedTask;
    }



    private Task<List<string>> GetStudentsNamesAsync()
    {
        var list = new List<string>()
        {
            "Kenan",
            "Ali",
            "Royal",
            "Tural",
            "Leyla",
            "Huseyn",
            "Huseyn",
            "Isa",
            "Emin",
            "Resul",
            "Vasif",
            "Miri",
            "Nihat"
        };

        return Task.FromResult(list);
    }




    private async void ClickBtn_Click_5(object sender, RoutedEventArgs e)
    {
        var names = await GetStudentsNamesAsync();
        // Thread.Sleep(5000);
        await Task.Delay(5000);
        txtBox.Text = string.Join("\n", names);
    }







    private async void ClickBtn_Click_4(object sender, RoutedEventArgs e)
    {
        var httpClient = new HttpClient();
        txtBox.Text = await httpClient.GetStringAsync("https://awardonline.com/");
        Title = Thread.CurrentThread.ManagedThreadId.ToString();
    }


    private void ClickBtn_Click_3(object sender, RoutedEventArgs e)
    {
        var httpClient = new HttpClient();
        var context = SynchronizationContext.Current;

        var result = httpClient.GetStringAsync("https://awardonline.com/")
            .ContinueWith(task =>
            {
                context?.Send(_ =>
                {
                    txtBox.Text = task.Result;
                    Title = Thread.CurrentThread.ManagedThreadId.ToString();
                }, null);
            });
    }



    private void ClickBtn_Click_2(object sender, RoutedEventArgs e)
    {
        var httpClient = new HttpClient();

        var result = httpClient.GetStringAsync("https://awardonline.com/")
            .ContinueWith(task =>
            {
                txtBox.Text = task.Result;
                Title = Thread.CurrentThread.ManagedThreadId.ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
    }





    private void DoSomethingLongRunning()
    {
        Thread.Sleep(3000);

        int id = Thread.CurrentThread.ManagedThreadId;

        Dispatcher.Invoke(() =>
        {
            txtBox.Text = id.ToString();
        });
    }


    private void ClickBtn_Click_1(object sender, RoutedEventArgs e)
    {
        Thread thread = new Thread(DoSomethingLongRunning);
        thread.Start();
    }
}
