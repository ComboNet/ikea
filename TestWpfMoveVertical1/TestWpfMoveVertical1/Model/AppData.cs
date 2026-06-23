using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace TestWpfMoveVertical1.Model;

public static class AppData
{
    public static ChartOption ChartOption { get; set; } = new();
}

[ObservableObject]
public partial class ChartOption
{
    public double Padding { get; } = 5.0;
    public ObservableCollection<ICartesianAxis> YAxes { get; set; } = new()
    {
        new Axis
        {
            NameTextSize = 14,
            TextSize = 14,
            IsVisible = true,
        }
    };
    public ObservableCollection<ICartesianAxis> XAxes { get; } = new()
    {
        new DateTimeAxis(TimeSpan.FromMinutes(1), date => date.ToString("HH:mm"))
        {
            NameTextSize = 14,
            TextSize = 14,
            IsVisible = true,
            UnitWidth = TimeSpan.FromMinutes(1).Ticks,
            MinStep =TimeSpan.FromMinutes(1).Ticks,
            SeparatorsPaint = new SolidColorPaint
            {
                Color = SKColors.LightGray,
                StrokeThickness = 2,
                PathEffect = new DashEffect(new float[] { 6, 6 }, 0)
            }
            // SeparatorsPaint = new SolidColorPaint(SKColors.LightGray) { StrokeThickness = 2, }
        }
        /*new DateTimeAxis(TimeSpan.FromMinutes(1), date => date.ToString("HH:mm"))
        {
            NameTextSize = 14,
        }*/
    };
    public DrawMarginFrame Frame { get; set; } = new DrawMarginFrame()
    {
        Stroke = new SolidColorPaint
        {
            Color = SKColors.White.WithAlpha(80),
            StrokeThickness = 1
        }
    };
    public SolidColorPaint LegendBackgroundPaint { get; set; } = new SolidColorPaint(new SKColor(240, 240, 240));
    public LegendPosition LegendPosition { get; } = LegendPosition.Hidden;
    public SolidColorPaint LegendTextPaint { get; set; } =
        new SolidColorPaint
        {
            Color = new SKColor(255, 255, 255),
            SKTypeface = SKTypeface.FromFamilyName("Roboto")
        };
    public SolidColorPaint TooltipTextPaint { get; set; } = new SolidColorPaint
    {
        Color = new SKColor(242, 244, 195),
        SKTypeface = SKTypeface.FromFamilyName("Roboto"),
    };
    public SolidColorPaint TooltipBackgroundPaint { get; } = new SolidColorPaint(new SKColor(72, 0, 50));
}