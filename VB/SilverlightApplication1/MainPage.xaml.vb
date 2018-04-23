Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Printing
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes

Namespace SilverlightApplication1
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			ListCar = New List(Of Car)()
			ListCar.Add(New Car() With {.Name = "Car1", .Chart = 1})
			ListCar.Add(New Car() With {.Name = "Car2", .Chart = 2})
			ListCar.Add(New Car() With {.Name = "Car3", .Chart = 3})
			DataContext = Me

		End Sub
		Private privateListCar As List(Of Car)
		Public Property ListCar() As List(Of Car)
			Get
				Return privateListCar
			End Get
			Set(ByVal value As List(Of Car))
				privateListCar = value
			End Set
		End Property

		Private Sub Button_Click_1(ByVal sender As Object, ByVal e As RoutedEventArgs)
		 '   TableView1.ShowPrintPreview(this);
			Dim preview As New DocumentPreview()
			Dim link As New PrintableControlLink(TryCast(grid.View, IPrintableControl))
			link.ExportServiceUri = "../ExportService.svc"
			Dim model As New LinkPreviewModel(link)
			preview.Model = model

			link.CreateDocument(True)
			Dim dlg As New DXDialog()
			dlg.Content = preview
			dlg.Height = 640
			dlg.Left = 150
			dlg.Top = 150
			dlg.ShowDialog()
		End Sub

		'ImageSource GetImage(string path) {
		'    return new BitmapImage(new Uri(path, UriKind.Relative));
		'}
	End Class

	Public Class Car

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		'public ImageSource Image { get; set; }
		Private privateChart As Double
		Public Property Chart() As Double
			Get
				Return privateChart
			End Get
			Set(ByVal value As Double)
				privateChart = value
			End Set
		End Property
	End Class

	Public NotInheritable Class Helper
        Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.RegisterAttached("Value", GetType(Object), GetType(Helper), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnValuePropertyChanged)))
		Private Sub New()
		End Sub
		Public Shared Function GetValue(ByVal obj As DependencyObject) As Object
			Return CObj(obj.GetValue(ValueProperty))
		End Function
		Public Shared Sub SetValue(ByVal obj As DependencyObject, ByVal value As Object)
			obj.SetValue(ValueProperty, value)
		End Sub
		Private Shared Sub OnValuePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			If Not(TypeOf e.NewValue Is Double) Then
				Return
			End If
			Dim ps As PieSeries2D = TryCast(d, PieSeries2D)
			If ps Is Nothing Then
				Return
			End If
			ps.Points.Clear()
			ps.Points.Add(New SeriesPoint With {.Value = CDbl(e.NewValue), .Argument = "Missing"})
			ps.Points.Add(New SeriesPoint With {.Value = 1, .Argument = "EnteredAnotherStation"})
			ps.Points.Add(New SeriesPoint With {.Value = 1, .Argument = "Entered"})
		End Sub
	End Class
End Namespace
