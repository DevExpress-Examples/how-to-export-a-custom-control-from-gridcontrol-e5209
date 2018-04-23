using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightApplication1 {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
            ListCar = new List<Car>();
            ListCar.Add(new Car() { Name = "Car1", Chart = 1 });
            ListCar.Add(new Car() { Name = "Car2", Chart = 2 });
            ListCar.Add(new Car() { Name = "Car3", Chart = 3 });
            DataContext = this;

        }
        public List<Car> ListCar { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
         //   TableView1.ShowPrintPreview(this);
            DocumentPreview preview = new DocumentPreview();
            PrintableControlLink link = new PrintableControlLink(grid.View as IPrintableControl);
            link.ExportServiceUri = "../ExportService.svc";
            LinkPreviewModel model = new LinkPreviewModel(link);
            preview.Model = model;

            link.CreateDocument(true);
            DXDialog dlg = new DXDialog();
            dlg.Content = preview;
            dlg.Height = 640;
            dlg.Left = 150;
            dlg.Top = 150;
            dlg.ShowDialog();
        }

        //ImageSource GetImage(string path) {
        //    return new BitmapImage(new Uri(path, UriKind.Relative));
        //}
    }

    public class Car {

        public string Name { get; set; }
        //public ImageSource Image { get; set; }
        public double Chart { get; set; }
    }

    public static class Helper {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(object), typeof(Helper), new PropertyMetadata(null, OnValuePropertyChanged));
        public static object GetValue(DependencyObject obj) {
            return (object)obj.GetValue(ValueProperty);
        }
        public static void SetValue(DependencyObject obj, object value) {
            obj.SetValue(ValueProperty, value);
        }
        static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (!(e.NewValue is double))
                return;
            PieSeries2D ps = d as PieSeries2D;
            if (ps == null)
                return;
            ps.Points.Clear();
            ps.Points.Add(new SeriesPoint { Value = (double)e.NewValue, Argument = "Missing" });
            ps.Points.Add(new SeriesPoint { Value = 1, Argument = "EnteredAnotherStation" });
            ps.Points.Add(new SeriesPoint { Value = 1, Argument = "Entered" });
        }
    }
}
